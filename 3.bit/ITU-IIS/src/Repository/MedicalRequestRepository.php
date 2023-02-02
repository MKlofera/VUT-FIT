<?php

namespace App\Repository;

use App\Entity\MedicalRequest;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

/**
 * @extends ServiceEntityRepository<MedicalRequest>
 *
 * @method MedicalRequest|null find($id, $lockMode = null, $lockVersion = null)
 * @method MedicalRequest|null findOneBy(array $criteria, array $orderBy = null)
 * @method MedicalRequest[]    findAll()
 * @method MedicalRequest[]    findBy(array $criteria, array $orderBy = null, $limit = null, $offset = null)
 */
class MedicalRequestRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, MedicalRequest::class);
    }

    public function save(MedicalRequest $entity, bool $flush = false): void
    {
        $this->getEntityManager()->persist($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(MedicalRequest $entity, bool $flush = false): void
    {
        $this->getEntityManager()->remove($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    //xsuman02
    public function getAllOpenRequests($filter = null)
    {
        $query = $this->createQueryBuilder('mr')
            ->leftJoin('mr.Vet', 'vet')
            ->leftJoin('mr.Dog', 'dog')
            ->leftJoin('mr.SocialWorker', 'wor')
            ->select('mr.id, mr.VetId, vet.Name as VetName, mr.DogId, dog.Name as DogName, mr.Status, mr.Priority, mr.Type, mr.Description, mr.RequestDate as Date, mr.SocialWorkerId, wor.Name as SocialWorkerName')
            ->where('mr.Status = 1');
        if (!is_null($filter)) {
            $query
                ->andWhere('vet.Name = :name')
                ->setParameter('name', $filter);
        }

        return $query
            ->getQuery()
            ->getResult();
    }

    //xsuman02
    public function getAllClosedRequests($filter = null)
    {
        $query = $this->createQueryBuilder('mr')
            ->leftJoin('mr.Vet', 'vet')
            ->leftJoin('mr.Dog', 'dog')
            ->leftJoin('mr.SocialWorker', 'wor')
            ->select('mr.id, mr.VetId, vet.Name as VetName, mr.DogId, dog.Name as DogName, mr.Status, mr.Priority, mr.Type, mr.Description, mr.RequestDate as Date, mr.SocialWorkerId, wor.Name as SocialWorkerName')
            ->where('mr.Status = 2');
        if (!is_null($filter)) {
            $query
                ->andWhere('vet.Name = :name')
                ->setParameter('name', $filter);
        }

        return $query
            ->getQuery()
            ->getResult();
    }

    public function getMedicalRequestDetailByID($id)
    {
        return $this->createQueryBuilder('mr')
            ->leftJoin('mr.Vet', 'vet')
            ->leftJoin('mr.Dog', 'dog')
            ->leftJoin('mr.SocialWorker', 'wor')
            ->select('mr.id, mr.VetId, vet.Name as VetName, mr.DogId, dog.Name as DogName, mr.Status, mr.Priority, mr.Type, mr.Description, mr.RequestDate as Date, mr.SocialWorkerId as SocialWorkerId, wor.Name as SocialWorkerName') //TODO Add ml.FinishedDate
            ->where('mr.id = :id')
            ->setParameter('id', $id)
            ->getQuery()
            ->getOneOrNullResult();
    }
}

//    /**
//     * @return MedicalRequest[] Returns an array of MedicalRequest objects
//     */
//    public function findByExampleField($value): array
//    {
//        return $this->createQueryBuilder('m')
//            ->andWhere('m.exampleField = :val')
//            ->setParameter('val', $value)
//            ->orderBy('m.id', 'ASC')
//            ->setMaxResults(10)
//            ->getQuery()
//            ->getResult()
//        ;
//    }

//    public function findOneBySomeField($value): ?MedicalRequest
//    {
//        return $this->createQueryBuilder('m')
//            ->andWhere('m.exampleField = :val')
//            ->setParameter('val', $value)
//            ->getQuery()
//            ->getOneOrNullResult()
//        ;
//    }
