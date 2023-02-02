<?php

namespace App\Entity;

use App\Repository\MedicalRequestRepository;
use Doctrine\DBAL\Types\Types;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: MedicalRequestRepository::class)]
class MedicalRequest
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\Column]
    private ?int $SocialWorkerId = null;

    #[ORM\Column]
    private ?int $VetId = null;

    #[ORM\Column]
    private ?int $DogId = null;

    #[ORM\Column]
    private ?int $Type = null;

    #[ORM\Column(type: Types::TEXT, nullable: true)]
    private ?string $Description = null;

    #[ORM\Column(type:"datetime")]
    private ?\DateTime $RequestDate = null;

    #[ORM\Column]
    private ?int $Priority = null;

    #[ORM\Column]
    private ?int $Status = null;

    #[ORM\ManyToOne(inversedBy: 'CreatedMedicalRequests')]
    #[ORM\JoinColumn(nullable: false)]
    private ?User $SocialWorker = null;

    #[ORM\ManyToOne(inversedBy: 'HasMedicalRequests')]
    #[ORM\JoinColumn(nullable: false)]
    private ?User $Vet = null;

    #[ORM\ManyToOne(inversedBy: 'medicalRequests')]
    #[ORM\JoinColumn(nullable: false)]
    private ?Dog $Dog = null;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getSocialWorkerId(): ?int
    {
        return $this->SocialWorkerId;
    }

    public function setSocialWorkerId(int $SocialWorkerId): self
    {
        $this->SocialWorkerId = $SocialWorkerId;

        return $this;
    }

    public function getVetId(): ?int
    {
        return $this->VetId;
    }

    public function setVetId(int $VetId): self
    {
        $this->VetId = $VetId;

        return $this;
    }

    public function getDogId(): ?int
    {
        return $this->DogId;
    }

    public function setDogId(int $DogId): self
    {
        $this->DogId = $DogId;

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

    public function getDescription(): ?string
    {
        return $this->Description;
    }

    public function setDescription(?string $Description): self
    {
        $this->Description = $Description;

        return $this;
    }

    public function getRequestDate(): ?\DateTime
    {
        return $this->RequestDate;
    }

    public function setRequestDate(\DateTime $RequestDate): self
    {
        $this->RequestDate = $RequestDate;

        return $this;
    }

    public function getPriority(): ?int
    {
        return $this->Priority;
    }

    public function setPriority(int $Priority): self
    {
        $this->Priority = $Priority;

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

    public function getSocialWorker(): ?User
    {
        return $this->SocialWorker;
    }

    public function setSocialWorker(?User $SocialWorker): self
    {
        $this->SocialWorker = $SocialWorker;

        return $this;
    }

    public function getVet(): ?User
    {
        return $this->Vet;
    }

    public function setVet(?User $Vet): self
    {
        $this->Vet = $Vet;

        return $this;
    }

    public function getDog(): ?Dog
    {
        return $this->Dog;
    }

    public function setDog(?Dog $Dog): self
    {
        $this->Dog = $Dog;

        return $this;
    }
}
