<?php

namespace App\Repository;

use App\Entity\Dog;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

/**
 * @extends ServiceEntityRepository<Dog>
 *
 * @method Dog|null find($id, $lockMode = null, $lockVersion = null)
 * @method Dog|null findOneBy(array $criteria, array $orderBy = null)
 * @method Dog[]    findAll()
 * @method Dog[]    findBy(array $criteria, array $orderBy = null, $limit = null, $offset = null)
 */
class DogRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, Dog::class);
    }

    public function save(Dog $entity, bool $flush = false): void
    {
        $this->getEntityManager()->persist($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(Dog $entity, bool $flush = false): void
    {
        $this->getEntityManager()->remove($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    //xklofe01
    public function getDogMedicalLogs($id)
    {
        return $this->createQueryBuilder('d')
            ->leftJoin('d.MedicalLogs', 'ml')
            ->leftJoin('ml.vet', 'v')
            ->select('ml.id, v.Name, ml.VetId, ml.Type, ml.Description, ml.FinishedDate as Date') //TODO Add ml.FinishedDate
            ->andWhere('d.id = :id')
            ->setParameter('id', $id)
            ->getQuery()
            ->getResult();
    }

    //xsuman02
    public function getDogMedicalRequests($id)
    {
        return $this->createQueryBuilder('d')
            ->leftJoin('d.medicalRequests', 'mr')
            ->leftJoin('mr.SocialWorker', 's')
            ->select('mr.id, s.Name as sName, mr.SocialWorkerId as sId, mr.Type, mr.Priority, mr.Description, mr.RequestDate as Date')
            ->andWhere('d.id = :id')
            ->setParameter('id', $id)
            ->getQuery()
            ->getResult();
    }

    //xklofe01
    public function findAllDogs()
    {
        return $this->createQueryBuilder('d')
            ->select('d.id, d.Name, d.Breed, d.Age, d.Photo, d.Sex, d.Price')
            ->getQuery()
            ->getResult();
    }

    //xszabo16
    public function findAllDogsList()
    {
        return $this->createQueryBuilder('d')
            ->select('d.id, d.Name')
            ->getQuery()
            ->getResult();
    }

    //xsuman02
    public function getDogsFiltered($data)
    {
        $name = $data["FormInput"];
        $breed = $data["Breed"];
        $max_age = $data["MaxAge"];
        $min_age = $data["MinAge"];
        $sex = $data["Sex"];
        $min_price = $data["MinPrice"];
        $max_price = $data["MaxPrice"];

        $query = $this->createQueryBuilder('d')
            ->select('d.id, d.Name, d.Breed, d.Age, d.Photo, d.Sex, d.Price')
            ->where('d.Age >= :min_age')
            ->setParameter('min_age', $min_age)
            ->andWhere('d.Age <= :max_age')
            ->setParameter('max_age', $max_age)
            ->andWhere('d.Price >= :min_price')
            ->setParameter('min_price', $min_price)
            ->andWhere('d.Price <= :max_price')
            ->setParameter('max_price', $max_price);

        if (strlen($name) > 0) {
            $query
                ->andWhere('d.Name = :name')
                ->setParameter('name', $name);
        }
        if (is_numeric(($breed))) {
            $query
                ->andWhere('d.Breed = :breed')
                ->setParameter('breed', (int)$breed);
        }
        if ($sex == "male") {
            $query
                ->andWhere('d.Sex = :sex')
                ->setParameter('sex', 1);
        }
        if ($sex == "female") {
            $query
                ->andWhere('d.Sex = :sex')
                ->setParameter('sex', 0);
        }
        return $query
            ->getQuery()
            ->getResult();
    }

    //xklofe01
    public function getDogDetailByID(int $id)
    {
        return $this->createQueryBuilder('d')
            ->select('d.id, d.Name, d.Breed, d.Age, d.Photo, d.Sex, d.Price, d.Vaccinated,
                d.Dewormed, d.IsCastrated, d.ChipNumber, d.Description, d.InShelterBcs')
            ->where('d.id = :id')
            ->setParameter('id', $id)
            ->getQuery()
            ->getResult();
    }


    //    /**
    //     * @return Dog[] Returns an array of Dog objects
    //     */
    //    public function findByExampleField($value): array
    //    {
    //        return $this->createQueryBuilder('d')
    //            ->andWhere('d.exampleField = :val')
    //            ->setParameter('val', $value)
    //            ->orderBy('d.id', 'ASC')
    //            ->setMaxResults(10)
    //            ->getQuery()
    //            ->getResult()
    //        ;
    //    }

    //    public function findOneBySomeField($value): ?Dog
    //    {
    //        return $this->createQueryBuilder('d')
    //            ->andWhere('d.exampleField = :val')
    //            ->setParameter('val', $value)
    //            ->getQuery()
    //            ->getOneOrNullResult()
    //        ;
    //    }
}
