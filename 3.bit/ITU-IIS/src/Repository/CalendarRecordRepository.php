<?php

namespace App\Repository;

use App\Entity\CalendarRecord;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

/**
 * @extends ServiceEntityRepository<CalendarRecord>
 *
 * @method CalendarRecord|null find($id, $lockMode = null, $lockVersion = null)
 * @method CalendarRecord|null findOneBy(array $criteria, array $orderBy = null)
 * @method CalendarRecord[]    findAll()
 * @method CalendarRecord[]    findBy(array $criteria, array $orderBy = null, $limit = null, $offset = null)
 */
class CalendarRecordRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, CalendarRecord::class);
    }

    public function save(CalendarRecord $entity, bool $flush = false): void
    {
        $this->getEntityManager()->persist($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    public function remove(CalendarRecord $entity, bool $flush = false): void
    {
        $this->getEntityManager()->remove($entity);

        if ($flush) {
            $this->getEntityManager()->flush();
        }
    }

    //xsuman02
    public function getAllOpenReservations($filter = null)
    {
        $query = $this->createQueryBuilder('c')
            ->leftJoin('c.Dog', 'd')
            ->leftJoin('c.User', 'u')
            ->select('c.id, c.TimestampFrom, c.TimestampTo, c.Type, c.Status, d.Name as DogName, d.id as DogId, u.Name as UserName, u.id as UserId')
            ->where('c.Type = 1 AND (c.Status = 0 OR c.Status = 1)');
        if (!is_null($filter)) {
            $query
                ->andWhere('u.Name = :name')
                ->setParameter('name', $filter);
        }

        return $query
            ->getQuery()
            ->getResult();
    }

    //xsuman02
    public function getAllClosedReservations($filter = null)
    {
        $query = $this->createQueryBuilder('c')
            ->leftJoin('c.Dog', 'd')
            ->leftJoin('c.User', 'u')
            ->select('c.id, c.TimestampFrom, c.TimestampTo, c.Type, c.Status, d.Name as DogName, d.id as DogId, u.Name as UserName, u.id as UserId')
            ->where('c.Type = 1 AND (c.Status = 2 OR c.Status = 3)');
        if (!is_null($filter)) {
            $query
                ->andWhere('u.Name = :name')
                ->setParameter('name', $filter);
        }

        return $query
            ->getQuery()
            ->getResult();
    }

    //xsuman02
    public function findByEmail($email)
    {
        return $this->createQueryBuilder('c')
            ->leftJoin('c.Dog', 'd')
            ->leftJoin('c.User', 'u')
            ->select('c.id, c.TimestampFrom, c.TimestampTo, c.Type, c.Status, d.id as DogId, d.Name as DogName, d.Breed as DogBreed, d.Sex as DogSex')
            ->where('c.Type = 1 AND u.Email = :email')
            ->setParameter('email', $email)
            ->getQuery()
            ->getResult();
    }

    //xszabo16
    public function findAllRecordsByDog()
    {
        return $this->createQueryBuilder('cr')
            ->select('cr.id, cr.TimestampFrom, cr.TimestampTo, cr.Type, cr.Status, d.id as DogId, d.Name as DogName, u.id as UserId, u.Name as OwnerName')
            ->leftJoin('cr.Dog', 'd')
            ->leftJoin('cr.User', 'u')
            ->where('cr.TimestampTo > :now')
            ->setParameter('now', new \Datetime("-1 day"))
            ->getQuery()
            ->getResult();
    }

    //    /**
    //     * @return CalendarRecord[] Returns an array of CalendarRecord objects
    //     */
    //    public function findByExampleField($value): array
    //    {
    //        return $this->createQueryBuilder('c')
    //            ->andWhere('c.exampleField = :val')
    //            ->setParameter('val', $value)
    //            ->orderBy('c.id', 'ASC')
    //            ->setMaxResults(10)
    //            ->getQuery()
    //            ->getResult()
    //        ;
    //    }

    //    public function findOneBySomeField($value): ?CalendarRecord
    //    {
    //        return $this->createQueryBuilder('c')
    //            ->andWhere('c.exampleField = :val')
    //            ->setParameter('val', $value)
    //            ->getQuery()
    //            ->getOneOrNullResult()
    //        ;
    //    }
}
