<?php

namespace App\Controller;

use App\Entity\MedicalRequest;
use App\Repository\DogRepository;
use App\Repository\MedicalRequestRepository;
use App\Repository\UserRepository;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpFoundation\File\UploadedFile;
use Symfony\Component\Routing\Annotation\Route;
use Symfony\Component\Serializer\Encoder\JsonEncoder;
use Symfony\Component\Serializer\Normalizer\ObjectNormalizer;
use Symfony\Component\Serializer\Serializer;
use DateTime;


#[Route('/API/medicalRequests')]
class MedicalRequestController extends AbstractController
{
    #[Route('/', name: 'app_medical_request_index', methods: ['GET'])]
    public function index(MedicalRequestRepository $medicalRequestRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allNewRegistratedUsersSqlResult = $medicalRequestRepository->getAllRequests();
        $allNewRegistratedUsersParsed = $this->parseMedicalRequests($allNewRegistratedUsersSqlResult);

        $jsonObject = $serializer->serialize($allNewRegistratedUsersParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/openRequests', name: 'app_medical_request_openRequests', methods: ['GET'])]
    public function getopenReservations(medicalRequestRepository $medicalRequestRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allOpenRequestsSqlResult = $medicalRequestRepository->getAllOpenRequests();
        $allOpenRequestsParsed = $this->parseMedicalRequests($allOpenRequestsSqlResult);


        $jsonObject = $serializer->serialize($allOpenRequestsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/closedRequests', name: 'app_medical_request_closedRequests', methods: ['GET'])]
    public function getclosedReservations(medicalRequestRepository $medicalRequestRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allClosedReservationsSqlResult = $medicalRequestRepository->getAllClosedRequests();
        $allClosedReservationsParsed = $this->parseMedicalRequests($allClosedReservationsSqlResult);


        $jsonObject = $serializer->serialize($allClosedReservationsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/deleteRequests', name: 'app_medical_request_deleteRequests', methods: ['DELETE'])]
    public function deleteReservations(medicalRequestRepository $medicalRequestRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $record) {
            try {
                $medicalRequestEntity = $medicalRequestRepository->find($record['id']);
                $medicalRequestRepository->remove($medicalRequestEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "Medical requests are not deleted"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "Medical requests are deleted}', 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/filtered', name: 'app_medical_request_filtered', methods: ['POST'])]
    public function calendar_filtered(medicalRequestRepository $medicalRequestRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);

        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);
        if ($data["WhichRequests"]) {
            $allOpenRequestsFilteredSqlResult = $medicalRequestRepository->getAllOpenRequests($data["FormInput"]);
            $allOpenRequestsFilteredParsed = $this->parseMedicalRequests($allOpenRequestsFilteredSqlResult);

            $jsonObject = $serializer->serialize($allOpenRequestsFilteredParsed, 'json', [
                'circular_reference_handler' => function ($object) {
                    return $object->getId();
                }
            ]);
        } else {
            $allClosedRequestsFilteredSqlResult = $medicalRequestRepository->getAllClosedRequests($data["FormInput"]);
            $allClosedRequestsFilteredParsed = $this->parseMedicalRequests($allClosedRequestsFilteredSqlResult);

            $jsonObject = $serializer->serialize($allClosedRequestsFilteredParsed, 'json', [
                'circular_reference_handler' => function ($object) {
                    return $object->getId();
                }
            ]);
        }


        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    // xsuman02
    #[Route('/new', name: 'app_medical_request_new', methods: ['GET', 'POST'])]
    public function new(Request $request, MedicalRequestRepository $medicalRequestRepository, DogRepository $dogRepository, UserRepository $userRepository): Response
    {
        $from = new DateTime();
        $from->format("Y-m-d h:i:s");

        $data = json_decode($request->getContent(), true);
        $medicalRequestEntity = new MedicalRequest();
        $medicalRequestEntity->setVet($userRepository->find($data['vetId']));
        $medicalRequestEntity->setDog($dogRepository->find($data['dogId']));
        $medicalRequestEntity->setSocialWorker($userRepository->find($data['workerId']));
        $medicalRequestEntity->setType($data['type']);
        $medicalRequestEntity->setStatus(1);
        $medicalRequestEntity->setPriority($data['priority']);
        $medicalRequestEntity->setRequestDate($from);
        $medicalRequestEntity->setDescription($data['description']);
        try {
            $medicalRequestRepository->save($medicalRequestEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "$medicalRequest not created"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "$medicalRequest created}', 200, ['Content-Type' => 'application/json']);
    }

    // xsuman02
    #[Route('/{id}', name: 'app_medical_request_show', methods: ['GET'])]
    public function show(MedicalRequestRepository $medicalRequestRepository, int $id): Response
    {
        if (filter_var($id, FILTER_VALIDATE_INT) === false) {
            return new Response(null, 404);
        }
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $medicalRequestsResult = $medicalRequestRepository->getMedicalRequestDetailByID($id);
        if ($medicalRequestsResult == null) {
            return new Response("Error: no medical request for this dog", 404);
        }
        $medicalRequestsParsed = $this->parseSingleMedicalRequest($medicalRequestsResult);

        $jsonObject = $serializer->serialize($medicalRequestsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);
        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    // xsuman02
    #[Route('/edit/{id}', name: 'app_medical_request_edit', methods: ['POST'])]
    public function edit(Request $request, MedicalRequestRepository $medicalRequestRepository, UserRepository $userRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        try {
            $medicalRequestEntity = $medicalRequestRepository->find($data['id']);
            $medicalRequestEntity->setVet($userRepository->find($data['vetId']));
            $medicalRequestEntity->setType($data['type']);
            $medicalRequestEntity->setDescription($data['text']);
            $medicalRequestEntity->setStatus($data['status']);
            $medicalRequestEntity->setPriority($data['prio']);
            $medicalRequestRepository->save($medicalRequestEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "$medicalRequest not updated"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "$medicalRequest updated}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/deleteRequests', name: 'app_medical_request_deleteRequests', methods: ['DELETE'])]
    public function deleteLogs(Request $request, MedicalRequestRepository $medicalRequestRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $singleId) {
            try {
                $medicalRequestEntity = $medicalRequestRepository->find($singleId['id']);
                $medicalRequestRepository->remove($medicalRequestEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "$medicalRequests are not deleted"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "$medicalRequests are deleted}', 200, ['Content-Type' => 'application/json']);
    }

    #[Route('/deleteById/{id}', name: 'app_medical_request_deleteById', methods: ['DELETE'])]
    public function deleteById(MedicalRequestRepository $medicalRequestRepository, int $id): Response
    {
        try {
            $medicalRequestEntity = $medicalRequestRepository->find($id);
            $medicalRequestRepository->remove($medicalRequestEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "medicalRequest not deleted"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "medicalRequests deleted}', 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    private function parseMedicalRequests($data): array
    {
        $returnArray = [];
        foreach ($data as $request) {
            $returnArray[] = [
                'id' => $request['id'],
                'user' => [
                    'id' => $request['VetId'],
                    'text' => $request['VetName'],
                ],
                'dog' => [
                    'id' => $request['DogId'],
                    'text' => $request['DogName'],
                ],

                'type' => $request["Type"],
                'prio' => $request['Priority'],
                'date' => date("Y-m-d H:i:s", $request["Date"]->getTimestamp()),
                'text' => $request['Description'],
                'status' => $request['Status']
            ];
        }
        return $returnArray;
    }


    //xsuman02
    private function parseSingleMedicalRequest($data): array
    {
        $returnArray = [
            'id' => $data['id'],
            'vet' => [
                'id' => $data['VetId'],
                'text' => $data['VetName'],
            ],
            'worker' => [
                'id' => $data['SocialWorkerId'],
                'text' => $data['SocialWorkerName'],
            ],
            'dog' => [
                'id' => $data['DogId'],
                'text' => $data['DogName'],
            ],

            'type' => $data["Type"],
            'prio' => $data['Priority'],
            'date' => date("Y-m-d H:i:s", $data["Date"]->getTimestamp()),
            'text' => $data['Description'],
            'status' => $data['Status']
        ];
        return $returnArray;
    }
}
