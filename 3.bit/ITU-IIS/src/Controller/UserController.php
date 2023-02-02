<?php

namespace App\Controller;

use App\Entity\User;
use App\Form\UserType;
use App\Repository\UserRepository;
use Symfony\Component\Serializer\Normalizer\ObjectNormalizer;
use Symfony\Component\Serializer\Serializer;
use Symfony\Component\Serializer\Encoder\JsonEncoder;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Serializer\Normalizer\AbstractNormalizer;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;

#[Route('API/user')]
class UserController extends AbstractController
{
    //xklofe01
    #[Route('/allNewRegistratedUsers', name: 'app_user_allNewRegistratedUsers', methods: ['GET'])]
    public function getAllNewRegistratedUsers(UserRepository $userRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allNewRegistratedUsersSqlResult = $userRepository->getAllNewRegistratedUsers();
        $allNewRegistratedUsersParsed = $this->parseUserList($allNewRegistratedUsersSqlResult);

        $jsonObject = $serializer->serialize($allNewRegistratedUsersParsed, 'json', [
            AbstractNormalizer::IGNORED_ATTRIBUTES => ['Email', 'Password'],
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/allActiveUsers', name: 'app_user_allActiveUsers', methods: ['GET'])]
    public function getAllActiveUsers(UserRepository $userRepository): Response
    {
        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $allActiveUsersSqlResult = $userRepository->getAllActiveUsers();
        $allActiveUsersParsed = $this->parseUserList($allActiveUsersSqlResult);

        $jsonObject = $serializer->serialize($allActiveUsersParsed, 'json', [
            AbstractNormalizer::IGNORED_ATTRIBUTES => ['Email', 'Password'],
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/changeUserRole', name: 'app_user_changeUsersRoles', methods: ['POST'])]
    public function changeUsersRoles(UserRepository $userRepository, Request $request): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $user) {
            try {
                $userEntity = $userRepository->find($user['id']);
                $userEntity->setRole($user['role']);
                $userRepository->save($userEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "Users roles not updated"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "Users roles updated}', 200, ['Content-Type' => 'application/json']);
    }

    // xszabo16
    #[Route('/signup', name: 'app_user_new', methods: ['POST'])]
    public function newUser(Request $request, UserRepository $userRepository): Response
    {
        // get the data from the request
        $user = new User();
        // get the data from the request
        $data = json_decode($request->getContent(), true);

        // validate the data
        if (empty($data['email']) || empty($data['username']) || empty($data['password'])) {
            return new Response('{"error": "Expecting mandatory parameters!"}', 404, ['Content-Type' => 'application/json']);
        }
        // validate email address is unique
        $existingUser = $userRepository->findOneBy(['Email' => $data['email']]);
        if ($existingUser) {
            return new Response('{"error": "Email address already exists!"}', 404, ['Content-Type' => 'application/json']);
        }
        $user->setEmail($data['email']);
        $user->setName($data['username']);
        $user->setPassword(password_hash($data['password'], PASSWORD_BCRYPT));
        $user->setRole(4);
        try {
            $userRepository->save($user, true);
            return new Response('{"message": "User created"}', 200, ['Content-Type' => 'application/json']);
        } catch (\Exception $e) {
            return new Response('{"error": "User not created"}', 500, ['Content-Type' => 'application/json']);
        }
    }

    #[Route('/{id}', name: 'app_user_show', methods: ['GET'])]
    public function show(string $id, UserRepository $userRepository): Response
    {
        if (filter_var($id, FILTER_VALIDATE_INT) === false) {
            return new Response(null, 404);
        }

        $encoders = [new JsonEncoder()];
        $normalizers = [new ObjectNormalizer()];
        $serializer = new Serializer($normalizers, $encoders);

        $jsonObject = $serializer->serialize($userRepository->getUserDetail($id), 'json', [
            AbstractNormalizer::IGNORED_ATTRIBUTES => ['Email', 'Password'],
            'circular_reference_handler' => function ($object) {
                return $object->getId();
            }
        ]);

        return new Response($jsonObject, 200, ['Content-Type' => 'application/json']);
    }

    #[Route('/{id}/edit', name: 'app_user_edit', methods: ['POST'])]
    public function edit(Request $request, User $user, UserRepository $userRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        try {
            $UserEntity = $userRepository->find($data['id']);
            $UserEntity->setName($data['Name']);
            $UserEntity->setRole($data['Role']);
            $userRepository->save($UserEntity, true);
        } catch (\Exception $e) {
            return new Response('{"error": "User is not updated"}', 500, ['Content-Type' => 'application/json']);
        }
        return new Response('{"message": "User is updated}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    #[Route('/deleteUsers', name: 'app_user_delete', methods: ['DELETE'])]
    public function delete(Request $request, UserRepository $userRepository): Response
    {
        $data = json_decode($request->getContent(), true);
        foreach ($data as $user) {
            try {
                $userEntity = $userRepository->find($user['id']);
                $userRepository->remove($userEntity, true);
            } catch (\Exception $e) {
                return new Response('{"error": "Users are not deleted"}', 500, ['Content-Type' => 'application/json']);
            }
        }
        return new Response('{"message": "Users are deleted}', 200, ['Content-Type' => 'application/json']);
    }

    //xklofe01
    private function parseUserList($data): array
    {
        $returnArray = [];
        foreach ($data as $user) {
            $returnArray[] = [
                'id' => $user['id'],
                'user' => [
                    'id' => $user['id'],
                    'text' => $user['Name'],
                ],
                'role' => $user["Role"],
                'email' => $user["Email"],
            ];
        }
        return $returnArray;
    }
}
