<?php

namespace App\Entity;

use App\Repository\DogRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\DBAL\Types\Types;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: DogRepository::class)]
class Dog
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\Column(length: 255)]
    private ?string $Name = null;

    #[ORM\Column]
    private ?int $Age = null;

    #[ORM\Column(type: Types::TEXT)]
    private ?string $Photo = null;

    #[ORM\Column]
    private ?int $Breed = null;

    #[ORM\Column]
    private ?bool $IsCastrated = null;

    #[ORM\Column(type: Types::TEXT)]
    private ?string $Description = null;

    #[ORM\Column]
    private ?int $InShelterBcs = null;

    #[ORM\OneToMany(mappedBy: 'Dog', targetEntity: CalendarRecord::class, orphanRemoval: true)]
    private Collection $CalendarRecords;

    #[ORM\OneToMany(mappedBy: 'Dog', targetEntity: MedicalLog::class, orphanRemoval: true)]
    private Collection $MedicalLogs;

    #[ORM\Column]
    private ?bool $Sex = null;

    #[ORM\Column]
    private ?bool $Dewormed = null;

    #[ORM\Column]
    private ?bool $Vaccinated = null;

    #[ORM\Column]
    private ?int $Price = null;

    #[ORM\Column(length: 15, nullable: true)]
    private ?string $ChipNumber = null;

    #[ORM\OneToMany(mappedBy: 'Dog', targetEntity: MedicalRequest::class, orphanRemoval: true)]
    private Collection $medicalRequests;

    public function __construct()
    {
        $this->CalendarRecords = new ArrayCollection();
        $this->MedicalLogs = new ArrayCollection();
        $this->medicalRequests = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getName(): ?string
    {
        return $this->Name;
    }

    public function setName(string $Name): self
    {
        $this->Name = $Name;

        return $this;
    }

    public function getAge(): ?int
    {
        return $this->Age;
    }

    public function setAge(int $Age): self
    {
        $this->Age = $Age;

        return $this;
    }

    public function getPhoto(): ?string
    {
        return $this->Photo;
    }

    public function setPhoto(string $Photo): self
    {
        $this->Photo = $Photo;

        return $this;
    }

    public function getBreed(): ?int
    {
        return $this->Breed;
    }

    public function setBreed(int $Breed): self
    {
        $this->Breed = $Breed;

        return $this;
    }

    public function isIsCastrated(): ?bool
    {
        return $this->IsCastrated;
    }

    public function setIsCastrated(bool $IsCastrated): self
    {
        $this->IsCastrated = $IsCastrated;

        return $this;
    }

    public function getDescription(): ?string
    {
        return $this->Description;
    }

    public function setDescription(string $Description): self
    {
        $this->Description = $Description;

        return $this;
    }

    public function getInShelterBcs(): ?int
    {
        return $this->InShelterBcs;
    }

    public function setInShelterBcs(int $InShelterBcs): self
    {
        $this->InShelterBcs = $InShelterBcs;

        return $this;
    }

    /**
     * @return Collection<int, CalendarRecord>
     */
    public function getCalendarRecords(): Collection
    {
        return $this->CalendarRecords;
    }

    public function setCalendarRecords(Collection $CalendarRecords): void
    {
        $this->CalendarRecords = $CalendarRecords;
    }


    public function addCalendarRecord(CalendarRecord $calendarRecord): self
    {
        if (!$this->CalendarRecords->contains($calendarRecord)) {
            $this->CalendarRecords->add($calendarRecord);
            $calendarRecord->setDogId($this);
        }

        return $this;
    }

    public function removeCalendarRecord(CalendarRecord $calendarRecord): self
    {
        if ($this->CalendarRecords->removeElement($calendarRecord)) {
            // set the owning side to null (unless already changed)
            if ($calendarRecord->getDogId() === $this) {
                $calendarRecord->setDogId(null);
            }
        }

        return $this;
    }

    /**
     * @return Collection<int, MedicalLog>
     */
    public function getMedicalLogs(): Collection
    {
        return $this->MedicalLogs;
    }

    public function setMedicalLogs(Collection $MedicalLogs): void
    {
        $this->MedicalLogs = $MedicalLogs;
    }

    public function addMedicalLog(MedicalLog $medicalLog): self
    {
        if (!$this->MedicalLogs->contains($medicalLog)) {
            $this->MedicalLogs->add($medicalLog);
            $medicalLog->setDog($this);
        }

        return $this;
    }

    public function removeMedicalLog(MedicalLog $medicalLog): self
    {
        if ($this->MedicalLogs->removeElement($medicalLog)) {
            // set the owning side to null (unless already changed)
            if ($medicalLog->getDog() === $this) {
                $medicalLog->setDog(null);
            }
        }

        return $this;
    }

    public function isSex(): ?bool
    {
        return $this->Sex;
    }

    public function setSex(bool $Sex): self
    {
        $this->Sex = $Sex;

        return $this;
    }

    public function isDewormed(): ?bool
    {
        return $this->Dewormed;
    }

    public function setDewormed(bool $Dewormed): self
    {
        $this->Dewormed = $Dewormed;

        return $this;
    }

    public function isVaccinated(): ?bool
    {
        return $this->Vaccinated;
    }

    public function setVaccinated(bool $Vaccinated): self
    {
        $this->Vaccinated = $Vaccinated;

        return $this;
    }

    public function getPrice(): ?int
    {
        return $this->Price;
    }

    public function setPrice(int $Price): self
    {
        $this->Price = $Price;

        return $this;
    }

    public function getChipNumber(): ?string
    {
        return $this->ChipNumber;
    }

    public function setChipNumber(?string $ChipNumber): self
    {
        $this->ChipNumber = $ChipNumber;

        return $this;
    }

    /**
     * @return Collection<int, MedicalRequest>
     */
    public function getMedicalRequests(): Collection
    {
        return $this->medicalRequests;
    }

    public function setMedicalRequests(Collection $medicalRequests): void
    {
        $this->medicalRequests = $medicalRequests;
    }

    public function addMedicalRequest(MedicalRequest $medicalRequest): self
    {
        if (!$this->medicalRequests->contains($medicalRequest)) {
            $this->medicalRequests->add($medicalRequest);
            $medicalRequest->setDog($this);
        }

        return $this;
    }

    public function removeMedicalRequest(MedicalRequest $medicalRequest): self
    {
        if ($this->medicalRequests->removeElement($medicalRequest)) {
            // set the owning side to null (unless already changed)
            if ($medicalRequest->getDog() === $this) {
                $medicalRequest->setDog(null);
            }
        }

        return $this;
    }
}
