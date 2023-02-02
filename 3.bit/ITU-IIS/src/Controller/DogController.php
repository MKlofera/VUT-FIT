<?php

namespace App\Controller;

use App\Entity\Dog;
use App\Form\DogType;
use App\Repository\DogRepository;
use Symfony\Component\Serializer\Normalizer\ObjectNormalizer;
use Symfony\Component\Serializer\Serializer;
use Symfony\Component\Serializer\Encoder\JsonEncoder;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('API/dog')]
class DogController extends AbstractController
{
    public $userRole = ["Admin", "Veterinář", "Dobrovolník", "Pečovatel", "Nový uživatel", "Undefined"];
    public $breed = [
        "Afgánský chrt", "Anglický buldok", "Argentinská doga", "Belgický ovčák", "Bígl", "Border kolie", "Bulteriér", "Československý vlčák", "Dalmatin", "Dobrman", "Holandský ovčák", "Jezevčík", "Německý ovčák", "Shiba-Inu", "Zlatý retriever"
    ];
    public $sex = ["Pes", "Fenka"];

    //xsuman02
    #[Route('/', name: 'app_dog_index', methods: ['GET'])]
    public function index(DogRepository $dogRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $jsonObject = $serializer->serialize($dogRepository->findAllDogs(), 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/filtered', name: 'app_dog_index_filtered', methods: ['POST'])]
    public function index_filtered(DogRepository $dogRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);

        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $jsonObject = $serializer->serialize($dogRepository->getDogsFiltered($data), 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    #[Route('/uploadImage', name: 'app_dog_uploadImage', methods: ['POST'])]
    public function uploadImage(Request $request): Response
    {
        return new Response(getcwd(), 200, ['Content-Type' => 'application/json']);

        $image = $request->files->get('my-image-file');
        $image->move('uploads', "idk.png");

        return new Response("asd", 200, ['Content-Type' => 'application/json']);

    }

    #[Route('/new', name: 'app_dog_new', methods: ['POST'])]
    public function new(Request $request, DogRepository $dogRepository): Response
    {
        try {
            $data = json_decode($request->getContent(), true);
            $dogEntity = new Dog();
            $dogEntity->setName($data['Name']);
            $dogEntity->setBreed($data['Breed']);
            $dogEntity->setAge($data['Age']);
            $dogEntity->setPhoto($data['Photo']);
            $dogEntity->setSex($data['Sex']);
            $dogEntity->setPrice($data['Price']);
            $dogEntity->setVaccinated($data['Vaccinated']);
            $dogEntity->setDewormed($data['Dewormed']);
            $dogEntity->setIsCastrated($data['IsCastrated']);
            $dogEntity->setChipNumber($data['ChipNumber']);
            $dogEntity->setDescription($data['Description']);
            $dogEntity->setInShelterBcs($data['InShelterBcs']);
            $dogRepository->save($dogEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "Dog is not updated"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "Dog is updated}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/detail/{id}', name: 'app_dog_show', methods: ['GET'])]
    public function show(string $id, DogRepository $dogRepository): Response
    {
        if (filter_var($id, FILTER_VALIDATE_INT) === false) {
            return new Response(null, 404);
        }
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $dogSqlResult = $dogRepository->getDogDetailByID($id);

        if ($dogSqlResult == null) {
            return new Response(null, 404);
        }

        $jsonObject = $serializer->serialize($dogSqlResult[0], 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);
        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/medicalLogs/{id}', name: 'app_dog_showMedicalLogs', methods: ['GET'])]
    public function showMedicalLogs(string $id, DogRepository $dogRepository): Response
    {
        if (filter_var($id, FILTER_VALIDATE_INT) === false) {
            return new Response(null, 404);
        }
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $medicalLogsResult = $dogRepository->getDogMedicalLogs($id);
        if ($medicalLogsResult == null) {
            return new Response("Error: no medical logs for this dog", 404);
        }
        $medicalLogsParsed = $this->parseMedicalLogs($medicalLogsResult);

        $jsonObject = $serializer->serialize($medicalLogsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);
        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xsuman02
    #[Route('/medicalRequests/{id}', name: 'app_dog_showMedicalRequests', methods: ['GET'])]
    public function showMedicalRequests(string $id, DogRepository $dogRepository): Response
    {
        if (filter_var($id, FILTER_VALIDATE_INT) === false) {
            return new Response(null, 404);
        }
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $medicalRequestsResult = $dogRepository->getDogMedicalRequests($id);
        if ($medicalRequestsResult == null) {
            return new Response("Error: no medical requests for this dog", 404);
        }
        $medicalRequestsParsed = $this->parseMedicalRequests($medicalRequestsResult);

        $jsonObject = $serializer->serialize($medicalRequestsParsed, 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);
        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/edit', name: 'app_dog_edit', methods: ['POST'])]
    public function edit(Request $request, DogRepository $dogRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        try {
            $dogEntity = $dogRepository->find($data['id']);
            $dogEntity->setName($data['Name']);
            $dogEntity->setBreed($data['Breed']);
            $dogEntity->setAge($data['Age']);
            $dogEntity->setPhoto($data['Photo']);
            $dogEntity->setSex($data['Sex']);
            $dogEntity->setPrice($data['Price']);
            $dogEntity->setVaccinated($data['Vaccinated']);
            $dogEntity->setDewormed($data['Dewormed']);
            $dogEntity->setIsCastrated($data['IsCastrated']);
            $dogEntity->setChipNumber($data['ChipNumber']);
            $dogEntity->setDescription($data['Description']);
            $dogEntity->setInShelterBcs($data['InShelterBcs']);
            $dogRepository->save($dogEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "Dog is not updated"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "Dog is updated}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/{id}', name: 'app_dog_delete', methods: ['DELETE'])]
    public function delete(Request $request, Dog $dog, DogRepository $dogRepository): Response
    {
        if ($dogRepository->getDogDetailByID($dog->getId()) == null) {
            return new Response('{"error": "Dog not found!"}', 404, ['Content-Type' => 'application/json']);
        }
        $dogRepository->remove($dog, true);
        return new Response('{"status": "Dog delete success"}', 200, ['Content-Type' => 'application/json']);
    }

    // xszabo16
    #[Route('/getDogList', name: 'app_dog_list', methods: ['GET'])]
    public function getDogList(DogRepository $dogRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $jsonObject = $serializer->serialize($dogRepository->findAllDogsList(), 'json', [
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }


    private function parseMedicalLogs($data): array
    {
        $returnArray = [];
        foreach ($data as $item) {
            $returnArray[] = [
                'id' => $item['id'],
                'user' => [
                    'id' => $item['VetId'],
                    'text' => $item['Name'],
                ],
                'type' => $item["Type"],
                'date' => date("Y-m-d H:i:s", $item["Date"]->getTimestamp()),
                'vet-zaznam' => [
                    'id' => $item['id'],
                    'text' => $item['Description'],
                ],
            ];
        }

        return $this->shortenMedicalLogDescription($returnArray);
    }

    //xsuman02
    private function parseMedicalRequests($data): array
    {
        $returnArray = [];
        foreach ($data as $item) {
            $returnArray[] = [
                'id' => $item['id'],
                'user' => [
                    'id' => $item['sId'],
                    'text' => $item['sName'],
                ],
                'type' => $item["Type"],
                'priority' => $item["Priority"],
                'date' => date("Y-m-d H:i:s", $item["Date"]->getTimestamp()),
                'vet-pozadavek' => [
                    'id' => $item['id'],
                    'text' => $item['Description'],
                ],
            ];
        }

        return $this->shortenMedicalLogDescription($returnArray);
    }

    //xklofe01
    private function shortenMedicalLogDescription($data): array
    {
        foreach ($data as $key => $value) {
            if (isset($value["vet-zaznam"]["text"]) && strlen($value['vet-zaznam']['text']) > 30) {
                $data[$key]['vet-zaznam']['text'] = substr($value['vet-zaznam']['text'], 0, 30) . '...';
            }
        }
        return $data;
    }
}
