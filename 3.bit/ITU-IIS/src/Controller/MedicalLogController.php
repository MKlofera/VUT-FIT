<?php

namespace App\Controller;

use App\Entity\MedicalLog;
use App\Form\MedicalLogType;
use App\Repository\DogRepository;
use App\Repository\MedicalLogRepository;
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


#[Route('/API/medicalLog')]
class MedicalLogController extends AbstractController
{
    #[Route('/', name: 'app_medical_log_index', methods: ['GET'])]
    public function index(MedicalLogRepository $medicalLogRepository): Response
    {
        return $this->render('medical_log/index.html.twig', [
            'medical_logs' => $medicalLogRepository->findAll(),
        ]);

    }

    #[Route('/new', name: 'app_medical_log_new', methods: ['GET', 'POST'])]
    public function new(Request $request, MedicalLogRepository $medicalLogRepository, DogRepository $dogRepository, UserRepository $userRepository): Response
    {

        $from = new DateTime();
        $from->format("Y-m-d h:i:s");

        $data = json_decode($request->getContent(), true);
        $medicalLogEntity = new MedicalLog();
        $medicalLogEntity->setVet($userRepository->find($data['vetId']));
        $medicalLogEntity->setDog($dogRepository->find($data['dogId']));
        $medicalLogEntity->setType($data['type']);
        $medicalLogEntity->setFinishedDate($from);
        $medicalLogEntity->setDescription($data['description']);
        try {
            $medicalLogRepository->save($medicalLogEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "$medicalLog not updated"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "$medicalLog updated}', 200, ['Content-Type' => 'application/json']);
    }


    #[Route('/{id}', name: 'app_medical_log_show', methods: ['GET'])]
    public function show(MedicalLogRepository $medicalLogRepository, Request $request, int $id): Response
    {
        if (filter_var($id, FILTER_VALIDATE_INT) === false) {
            return new Response(null, 404);
        }
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $medicalLogsResult = $medicalLogRepository->getMedicalLogDetailByID($id);
        if ($medicalLogsResult == null) {
            return new Response("Error: no medical logs for this dog", 404);
        }
        $medicalLogsParsed = $this->parseMedicalLogs($medicalLogsResult);

        $jsonObject = $serializer->serialize($medicalLogsParsed[0], 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);
        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    #[Route('/edit/{id}', name: 'app_medical_log_edit', methods: ['POST'])]
    public function edit(Request $request, MedicalLog $medicalLog, MedicalLogRepository $medicalLogRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        try {
            $medicalLogEntity = $medicalLogRepository->find($data['id']);
            $medicalLogEntity->setType($data['type']);
            $medicalLogEntity->setDescription($data['text']);
            $medicalLogRepository->save($medicalLogEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "$medicalLog not updated"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "$medicalLog updated}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/deleteLogs', name: 'app_user_deleteLogs', methods: ['DELETE'])]
    public function deleteLogs(Request $request, MedicalLogRepository $medicalLogRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $singleId) {
            try {
                $medicalLogEntity = $medicalLogRepository->find($singleId['id']);
                $medicalLogRepository->remove($medicalLogEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "$medicalLogEntity are not deleted"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "$medicalLogEntity are deleted}', 200, ['Content-Type' => 'application/json']);
    }

    #[Route('/deleteById/{id}', name: 'app_user_deleteById', methods: ['DELETE'])]
    public function deleteById(Request $request, MedicalLogRepository $medicalLogRepository, int $id): Response
    {
        try {
            $medicalLogEntity = $medicalLogRepository->find($id);
            $medicalLogRepository->remove($medicalLogEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "Users are not deleted"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "Users are deleted}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    private function parseMedicalLogs($data): array
    {
        $returnArray = [];
        $returnArray[] = [
            'id' => $data['id'],
            'user' => [
                'id' => $data['VetId'],
                'text' => $data['VetName'],
            ],
            'dog' => [
                'id' => $data['DogId'],
                'text' => $data['DogName'],
            ],
            'type' => $data["Type"],
            'text' => $data['Description'],
            'date' => date("Y-m-d H:i:s", $data["Date"]->getTimestamp())
        ];
        return $returnArray;
    }
}
