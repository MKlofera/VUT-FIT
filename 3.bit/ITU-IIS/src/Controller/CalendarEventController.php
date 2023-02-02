<?php // Tomáš Szabó, xszabo16

namespace App\Controller;

use App\Entity\CalendarRecord;
use App\Repository\CalendarRecordRepository;
use App\Repository\DogRepository;
use Error;
use Exception;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Serializer\Normalizer\AbstractNormalizer;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\Serializer\Encoder\JsonEncoder;
use Symfony\Component\Serializer\Normalizer\DateTimeNormalizer;
use Symfony\Component\Serializer\Normalizer\ObjectNormalizer;
use Symfony\Component\Serializer\Serializer;
use Symfony\Component\Validator\Constraints\DateTime;

#[Route('/API/calendarEvent')]
class CalendarEventController extends AbstractController
{
    //xsuman02
    #[Route('/', name: 'app_calendarEvent_index', methods: ['GET'])]
    public function index(CalendarRecordRepository $calendarRecordRepository): Response
    {
        $calendarRecords = $calendarRecordRepository->findAllRecordsByDog();

        $encoders = [new JsonEncoder()];
        $normalizers = [new DateTimeNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $jsonString = $serializer->serialize($calendarRecords, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);
        return new Response($jsonString, 200,  ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/openReservations', name: 'app_calendarEvent_openReservations', methods: ['GET'])]
    public function getopenReservations(CalendarRecordRepository $calendarRecordRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allOpenReservationsSqlResult = $calendarRecordRepository->getAllOpenReservations();
        $allOpenReservationsParsed = $this->parseReservationRequests($allOpenReservationsSqlResult);


        $jsonObject = $serializer->serialize($allOpenReservationsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/closedReservations', name: 'app_calendarEvent_closedReservations', methods: ['GET'])]
    public function getclosedReservations(CalendarRecordRepository $calendarRecordRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allClosedReservationsSqlResult = $calendarRecordRepository->getAllClosedReservations();
        $allClosedReservationsParsed = $this->parseReservationRequests($allClosedReservationsSqlResult);


        $jsonObject = $serializer->serialize($allClosedReservationsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/filtered', name: 'app_calendarEvent_filtered', methods: ['POST'])]
    public function calendar_filtered(CalendarRecordRepository $calendarRecordRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);

        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);
        if ($data["WhichReservations"]) {
            $allOpenReservationsFilteredSqlResult = $calendarRecordRepository->getAllOpenReservations($data["FormInput"]);
            $allOpenReservationsFilteredParsed = $this->parseReservationRequests($allOpenReservationsFilteredSqlResult);

            $jsonObject = $serializer->serialize($allOpenReservationsFilteredParsed, 'json', [
                'circular_reference_handler' => function ($object) {
                    return $object->getId();
                }
            ]);
        } else {
            $allClosedReservationsFilteredSqlResult = $calendarRecordRepository->getAllClosedReservations($data["FormInput"]);
            $allClosedReservationsFilteredParsed = $this->parseReservationRequests($allClosedReservationsFilteredSqlResult);

            $jsonObject = $serializer->serialize($allClosedReservationsFilteredParsed, 'json', [
                'circular_reference_handler' => function ($object) {
                    return $object->getId();
                }
            ]);
        }


        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/changeStatus', name: 'app_calendarEvent_changeStatus', methods: ['POST'])]
    public function changeReservations(CalendarRecordRepository $calendarRecordRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $record) {
            try {
                $calendarRecordEntity = $calendarRecordRepository->find($record['id']);
                $calendarRecordEntity->setStatus($record['status']);
                $calendarRecordRepository->save($calendarRecordEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "Calendar records status not updated"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "Calendar records status updated}', 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/deleteReservations', name: 'app_calendarEvent_deleteReservations', methods: ['DELETE'])]
    public function deleteReservations(CalendarRecordRepository $calendarRecordRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $record) {
            try {
                $calendarRecordEntity = $calendarRecordRepository->find($record['id']);
                $calendarRecordRepository->remove($calendarRecordEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "Calendar records are not deleted"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "Calendar records are deleted}', 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/walkingHistory', name: 'app_calendarEvent_walkingHistory', methods: ['GET'])]
    public function getWalkingHistory(CalendarRecordRepository $calendarRecordRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $user = $this->getUser()->getUserIdentifier();
        $allWalkingRecordsResult = $calendarRecordRepository->findByEmail($user);
        $allWalkingRecordsParsed = $this->parseWalkingHistory($allWalkingRecordsResult);


        $jsonObject = $serializer->serialize($allWalkingRecordsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    // xszabo16
    #[Route('/add', name: 'app_calendarEvent_new', methods: ['POST'])]
    public function addCalendarEvent(Request $request, CalendarRecordRepository $calendarRecordRepository, DogRepository $dogRepository): Response
    {
        $this->denyAccessUnlessGranted('IS_AUTHENTICATED_FULLY');
        $calendarRecord = new CalendarRecord();

        $data = json_decode($request->getContent(), true);

        if ($err = $this->validateData($data)) {
            return $err;
        }
        $this->setDataFromRequest($calendarRecord, $data, $dogRepository);

        try {
            $calendarRecordRepository->save($calendarRecord, true);
            return new Response('{"message": "Calendar Record created!"}', 200, ['Content-Type' => 'application/json']);
        } catch (\Exception $e) {
            return new Response('{"error": "Calendar Record not created!"}', 500, ['Content-Type' => 'application/json']);
        }
    }

    // xszabo16
    #[Route('/edit/{id}', name: 'app_calendarEvent_edit', methods: ['POST'])]
    public function edit(Request $request, ?int $id, CalendarRecordRepository $calendarRecordRepository, DogRepository $dogRepository): Response
    {
        $this->denyAccessUnlessGranted('IS_AUTHENTICATED_FULLY');
        if (!is_numeric($id)) {
            return new Response('{"error": "Expecting mandatory parameters!"}', 404, ['Content-Type' => 'application/json']);
        }

        $calendarRecord = $calendarRecordRepository->find($id);
        if (!$calendarRecord) {
            return new Response('{"error": "Calendar Record with id ' . $id . 'not found!"}', 404, ['Content-Type' => 'application/json']);
        }
        $data = json_decode($request->getContent(), true);
        
        if ($err = $this->validateData($data)) {
            return $err;
        }

        $this->setDataFromRequest($calendarRecord, $data, $dogRepository);
        try {
            $calendarRecordRepository->save($calendarRecord, true);
            return new Response('{"message": "Calendar Record with updated!"}', 200, ['Content-Type' => 'application/json']);
        } catch (\Exception $e) {
            return new Response('{"error": "Calendar Record with id ' . $id . ' not updated!"}', 500, ['Content-Type' => 'application/json']);
        }
    }

    // xszabo16
    #[Route('/delete/{id}', name: 'app_calendarEvent_delete', methods: ['DELETE'])]
    public function deleteCalendarEvent(?int $id, CalendarRecordRepository $calendarRecordRepository): Response
    {
        $this->denyAccessUnlessGranted('IS_AUTHENTICATED_FULLY');

        if (!is_numeric($id)) {
            return new Response('{"error": "Expecting mandatory parameters!"}', 404, ['Content-Type' => 'application/json']);
        }
        $calendarRecord = $calendarRecordRepository->find($id);
        if (!$calendarRecord) {
            return new Response('{"error": "Calendar Record with id ' . $id . ' not found!"}', 404, ['Content-Type' => 'application/json']);
        }
        try {
            $calendarRecordRepository->remove($calendarRecord, true);
            return new Response('{"message": "Calendar Record with id ' . $id . ' deleted"}', 200, ['Content-Type' => 'application/json']);
        } catch (\Exception $e) {
            return new Response('{"error": "Calendar Record with id ' . $id . ' not deleted!"}', 404, ['Content-Type' => 'application/json']);
        }
    }

    // xszabo16
    private function setDataFromRequest(CalendarRecord $calendarRecord, mixed $data, DogRepository $dogRepository)
    {
        $calendarRecord->setDogId($dogRepository->find($data['dog']));
        $calendarRecord->setUserId($this->getUser());
        $calendarRecord->setTimestampFrom(new \DateTime($data['timestampFrom'], new \DateTimeZone('UTC')));
        $calendarRecord->setTimestampTo(new \DateTime($data['timestampTo'], new \DateTimeZone('UTC')));
        $calendarRecord->setStatus($data['status']);
        $calendarRecord->setType($data['type']);
    }

    //xsuman02
    private function parseReservationRequests($data): array
    {
        $returnArray = [];
        foreach ($data as $record) {
            $returnArray[] = [
                'id' => $record['id'],
                'user' => [
                    'id' => $record['UserId'],
                    'text' => $record['UserName'],
                ],
                'dog' => [
                    'id' => $record['DogId'],
                    'text' => $record['DogName'],
                ],
                'timestampFrom' => date("Y-m-d H:i:s", $record["TimestampFrom"]->getTimestamp()),
                'timestampTo' => date("Y-m-d H:i:s", $record["TimestampTo"]->getTimestamp()),
                'status' => $record['Status']
            ];
        }
        return $returnArray;
    }

    //xsuman02
    private function parseWalkingHistory($data): array
    {
        $returnArray = [];
        foreach ($data as $record) {
            $returnArray[] = [
                'id' => $record['id'],
                'dog' => [
                    'id' => $record['DogId'],
                    'text' => $record['DogName'],
                ],
                'breed' => $record['DogBreed'],
                'sex' => $record['DogSex'] ? 1 : 0,
                'date' => date("Y-m-d", $record["TimestampFrom"]->getTimestamp()),
                'timeFrom' => date("H:i", $record["TimestampFrom"]->getTimestamp()),
                'timeTo' => date("H:i", $record["TimestampTo"]->getTimestamp()),
                'status' => $record['Status']
            ];
        }
        return $returnArray;
    }

    // xszabo16
    private function validateData($data)
    {
        foreach ($data as $value) {
            if ($value === null) {
                return new Response('{"error": "Expecting mandatory parameters!"}', 404, ['Content-Type' => 'application/json']);
            }
        }

        if ($data["timestampFrom"] === $data["timestampTo"]) {
            return new Response('{"error": "Time is not valid!"}', 404, ['Content-Type' => 'application/json']);
        }

        $dateTimeObject = strtotime($data["timestampFrom"]);
        $dateTimeObject2 = strtotime($data["timestampTo"]);
        if ($dateTimeObject > $dateTimeObject2) {
            return new Response('{"error": "Time is not valid!"}', 404, ['Content-Type' => 'application/json']);
        }
        return null;
    }
}
