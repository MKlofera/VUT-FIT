<?php

namespace App\Entity;

use App\Repository\MedicalLogRepository;
use Doctrine\DBAL\Types\Types;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: MedicalLogRepository::class)]
class MedicalLog
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\Column]
    private ?int $VetId = null;

    #[ORM\Column]
    private ?int $DogId = null;

    #[ORM\Column]
    private ?int $Type = null;

    #[ORM\Column(type:"datetime")]
    private ?\DateTime $FinishedDate = null;

    #[ORM\Column(type: Types::TEXT, nullable: true)]
    private ?string $Description = null;

    #[ORM\ManyToOne(inversedBy: 'MedicalLogs')]
    #[ORM\JoinColumn(nullable: false)]
    private ?User $vet = null;

    #[ORM\ManyToOne(inversedBy: 'MedicalLogs')]
    #[ORM\JoinColumn(nullable: false)]
    private ?Dog $Dog = null;

    public function getId(): ?int
    {
        return $this->id;
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

    public function getFinishedDate(): ?\DateTime
    {
        return $this->FinishedDate;
    }

    public function setFinishedDate(\DateTime $FinishedDate): self
    {
        $this->FinishedDate = $FinishedDate;

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

    public function getVet(): ?User
    {
        return $this->vet;
    }

    public function setVet(?User $vet): self
    {
        $this->vet = $vet;

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
