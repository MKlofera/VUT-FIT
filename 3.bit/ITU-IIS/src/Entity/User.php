<?php

namespace App\Entity;

use App\Repository\UserRepository;
use Symfony\Component\Security\Core\User\UserInterface;
use Symfony\Component\Security\Core\User\PasswordAuthenticatedUserInterface;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: UserRepository::class)]
#[ORM\Table(name: '`user`')]
class User implements UserInterface, PasswordAuthenticatedUserInterface
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column]
    private ?int $id = null;

    #[ORM\Column]
    private ?int $Role = null;

    #[ORM\Column(length: 255)]
    private ?string $Name = null;

    #[ORM\Column(length: 255)]
    private ?string $Email = null;

    #[ORM\Column(length: 255)]
    private ?string $Password = null;

    #[ORM\OneToMany(mappedBy: 'User', targetEntity: CalendarRecord::class, orphanRemoval: true)]
    private Collection $CalendarRecords;

    #[ORM\OneToMany(mappedBy: 'SocialWorker', targetEntity: MedicalRequest::class, orphanRemoval: true)]
    private Collection $CreatedMedicalRequests;

    #[ORM\OneToMany(mappedBy: 'Vet', targetEntity: MedicalRequest::class, orphanRemoval: true)]
    private Collection $HasMedicalRequests;

    #[ORM\OneToMany(mappedBy: 'vet', targetEntity: MedicalLog::class)]
    private Collection $MedicalLogs;

    public function __construct()
    {
        $this->CalendarRecords = new ArrayCollection();
        $this->MedicalRequests = new ArrayCollection();
        $this->CreatedMedicalRequests = new ArrayCollection();
        $this->HasMedicalRequests = new ArrayCollection();
        $this->MedicalLogs = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getRole(): ?int
    {
        return $this->Role;
    }

    public function setRole(int $Role): self
    {
        $this->Role = $Role;

        return $this;
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

    public function getEmail(): ?string
    {
        return $this->Email;
    }

    public function setEmail(string $Email): self
    {
        $this->Email = $Email;

        return $this;
    }

    public function getPassword(): ?string
    {
        return $this->Password;
    }

    public function setPassword(string $Password): self
    {
        $this->Password = $Password;

        return $this;
    }

    /**
     * @return Collection<int, CalendarRecord>
     */
    public function getCalendarRecords(): Collection
    {
        return $this->CalendarRecords;
    }

    public function addCalendarRecord(CalendarRecord $calendarRecord): self
    {
        if (!$this->CalendarRecords->contains($calendarRecord)) {
            $this->CalendarRecords->add($calendarRecord);
            $calendarRecord->setUserId($this);
        }

        return $this;
    }

    public function removeCalendarRecord(CalendarRecord $calendarRecord): self
    {
        if ($this->CalendarRecords->removeElement($calendarRecord)) {
            // set the owning side to null (unless already changed)
            if ($calendarRecord->getUserId() === $this) {
                $calendarRecord->setUserId(null);
            }
        }

        return $this;
    }

    /**
     * @return Collection<int, MedicalRequest>
     */
    public function getCreatedMedicalRequests(): Collection
    {
        return $this->CreatedMedicalRequests;
    }

    public function addCreatedMedicalRequest(MedicalRequest $createdMedicalRequest): self
    {
        if (!$this->CreatedMedicalRequests->contains($createdMedicalRequest)) {
            $this->CreatedMedicalRequests->add($createdMedicalRequest);
            $createdMedicalRequest->setSocialWorker($this);
        }

        return $this;
    }

    public function removeCreatedMedicalRequest(MedicalRequest $createdMedicalRequest): self
    {
        if ($this->CreatedMedicalRequests->removeElement($createdMedicalRequest)) {
            // set the owning side to null (unless already changed)
            if ($createdMedicalRequest->getSocialWorker() === $this) {
                $createdMedicalRequest->setSocialWorker(null);
            }
        }

        return $this;
    }

    /**
     * @return Collection<int, MedicalRequest>
     */
    public function getHasMedicalRequests(): Collection
    {
        return $this->HasMedicalRequests;
    }

    public function addHasMedicalRequest(MedicalRequest $hasMedicalRequest): self
    {
        if (!$this->HasMedicalRequests->contains($hasMedicalRequest)) {
            $this->HasMedicalRequests->add($hasMedicalRequest);
            $hasMedicalRequest->setVet($this);
        }

        return $this;
    }

    public function removeHasMedicalRequest(MedicalRequest $hasMedicalRequest): self
    {
        if ($this->HasMedicalRequests->removeElement($hasMedicalRequest)) {
            // set the owning side to null (unless already changed)
            if ($hasMedicalRequest->getVet() === $this) {
                $hasMedicalRequest->setVet(null);
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

    public function addMedicalLog(MedicalLog $medicalLog): self
    {
        if (!$this->MedicalLogs->contains($medicalLog)) {
            $this->MedicalLogs->add($medicalLog);
            $medicalLog->setVet($this);
        }

        return $this;
    }

    public function removeMedicalLog(MedicalLog $medicalLog): self
    {
        if ($this->MedicalLogs->removeElement($medicalLog)) {
            // set the owning side to null (unless already changed)
            if ($medicalLog->getVet() === $this) {
                $medicalLog->setVet(null);
            }
        }

        return $this;
    }

    // xszabo16
    public function getRoles() : array
    {
        switch ($this->getRole()) {
            case 4:
                return ['ROLE_REQUESTING'];
                break;
            case 3:
                return ['ROLE_VOLUNTEER'];
                break;
            case 2:
                return ['ROLE_SOCIAL_WORKER'];
                break;
            case 1:
                return ['ROLE_VET'];
                break;
            case 0:
                return ['ROLE_ADMIN'];
                break;
            default:
                return [];
                break;
        }
    }

    public function eraseCredentials()
    {
    }
    public function getUserIdentifier() : string
    {
        return $this->Email;
    }
}
