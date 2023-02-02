<?php

namespace App\Repository;

use App\Entity\MedicalLog;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

/**
 * @extends ServiceEntityRepository<MedicalLog>
 *
 * @method MedicalLog|null find($id, $lockMode = null, $lockVersion = null)
 * @method MedicalLog|null findOneBy(array $criteria, array $orderBy = null)
 * @method MedicalLog[]    findAll()
 * @method MedicalLog[]    findBy(array $criteria, array $orderBy = null, $limit = null, $offset = null)
 */
class MedicalLogRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, MedicalLog::class);
    }

    public function save(MedicalLog $entity, bool $flush = false): void
    {
        $this->getEntityManager()->persist($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(MedicalLog $entity, bool $flush = false): void
    {
        $this->getEntityManager()->remove($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    //xklofe01
    public function getMedicalLogDetailByID(int $id)
    {
//        return $this->createQueryBuilder('ml')
//            ->leftJoin('ml.vet', 'vet')
//            ->leftJoin('ml.Dog', 'dog')
////            ->select('ml.id, ml.$VetId, vet.Name as VetName, ml.$DogId, dog.Name as DogName, ml.Type, ml.Description') //TODO Add ml.FinishedDate
//            ->select('ml.id, ml.$VetId, ml.$DogId, ml.Type, ml.Description') //TODO Add ml.FinishedDate
//            ->andWhere('ml.id = :id')
//            ->setParameter('id', $id)
//            ->getQuery()
//            ->getResult();

        return $this->createQueryBuilder('ml')
            ->leftJoin('ml.vet', 'vet')
            ->leftJoin('ml.Dog', 'dog')
            ->select('ml.id, ml.VetId, vet.Name as VetName, ml.DogId, dog.Name as DogName, ml.Type, ml.Description, ml.FinishedDate as Date') //TODO Add ml.FinishedDate
            ->where('ml.id = :id')
            ->setParameter('id',$id)
            ->getQuery()
            ->getOneOrNullResult();

    }

//    /**
//     * @return MedicalLog[] Returns an array of MedicalLog objects
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

//    public function findOneBySomeField($value): ?MedicalLog
//    {
//        return $this->createQueryBuilder('m')
//            ->andWhere('m.exampleField = :val')
//            ->setParameter('val', $value)
//            ->getQuery()
//            ->getOneOrNullResult()
//        ;
//    }
}
