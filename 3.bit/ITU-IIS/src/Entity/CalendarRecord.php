<?php

namespace App\Entity;

use App\Repository\CalendarRecordRepository;
use Doctrine\DBAL\Types\Types;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: CalendarRecordRepository::class)]
class CalendarRecord
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\ManyToOne(inversedBy: 'CalendarRecords')]
    #[ORM\JoinColumn(nullable: false)]
    private ?Dog $Dog = null;

    #[ORM\ManyToOne(inversedBy: 'CalendarRecords')]
    #[ORM\JoinColumn(nullable: false)]
    private ?User $User = null;

    #[ORM\Column(type:"datetime")]
    private ?\DateTime $TimestampFrom = null;

    #[ORM\Column(type:"datetime")]
    private ?\DateTime $TimestampTo = null;

    #[ORM\Column]
    private ?int $Type = null;

    #[ORM\Column]
    private ?int $Status = null;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getDogId(): int
    {
        return $this->Dog->getId();
    }

    public function setDogId(?Dog $Dog): self
    {
        $this->Dog = $Dog;

        return $this;
    }

    public function getUserId(): int
    {
        return $this->User->getId();
    }

    public function setUserId(?User $User): self
    {
        $this->User = $User;

        return $this;
    }

    public function getTimestampFrom(): ?\DateTime
    {
        return $this->TimestampFrom;
    }

    public function setTimestampFrom(\DateTime $TimestampFrom): self
    {
        $this->TimestampFrom = $TimestampFrom;

        return $this;
    }

    public function getTimestampTo(): ?\DateTime
    {
        return $this->TimestampTo;
    }

    public function setTimestampTo(\DateTime $TimestampTo): self
    {
        $this->TimestampTo = $TimestampTo;

        return $this;
    }

    public function getType(): ?int
    {
        return $this->Type;
    }

    public function setType(int $Type): self
    {
        $this->Type = $Type;

        return $this;
    }

    public function getStatus(): ?int
    {
        return $this->Status;
    }

    public function setStatus(int $Status): self
    {
        $this->Status = $Status;

        return $this;
    }
}
