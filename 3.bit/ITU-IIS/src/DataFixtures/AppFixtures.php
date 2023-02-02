<?php
//xsuman02

namespace App\DataFixtures;

use App\Entity\Dog;
use App\Entity\User;
use App\Entity\CalendarRecord;
use App\Entity\MedicalLog;
use App\Entity\MedicalRequest;
use Doctrine\Bundle\FixturesBundle\Fixture;
use Doctrine\Persistence\ObjectManager;
use DateTime;

class AppFixtures extends Fixture
{
    public function load(ObjectManager $manager): void
    {
        /////////////// User - Admin ///////////////
        $admin = new User();
        $admin->setRole(0);
        $admin->setName("Tomáš Szabó");
        $admin->setEmail("admin@email.cz");
        $admin->setPassword("$2y$13$6jxVW8cceWzjtzz0TasEC.KsKA0Cyfckp87kB1kx1VrmQD5d5Q32O"); //admin
        $manager->persist($admin);
        ////////////////////////////////////////////

        /////////////// User - Social Worker 1 /////
        $worker_1 = new User();
        $worker_1->setRole(2);
        $worker_1->setName("Pavel Čmelák");
        $worker_1->setEmail("pcmelak@email.cz");
        $worker_1->setPassword("$2y$13\$ZLEnazuAQwLXU77qO55rsup8ln2/rcPfhxhEcksZPAojtw9TNZYuC"); //pcmelak
        $manager->persist($worker_1);
        ////////////////////////////////////////////

        /////////////// User - Social Worker 2 /////
        $worker_2 = new User();
        $worker_2->setRole(2);
        $worker_2->setName("Michal Drahý");
        $worker_2->setEmail("mdrahy@email.cz");
        $worker_2->setPassword("$2y$13$4UR85v9a7T89aKv4pqMlKuGvcMT8tyJqCfEOxqtWqnfu8MQpqaljq"); //mdrahy
        $manager->persist($worker_2);
        ////////////////////////////////////////////

        /////////////// User - Social Worker 3 /////
        $worker_3 = new User();
        $worker_3->setRole(2);
        $worker_3->setName("Social Worker test");
        $worker_3->setEmail("socialWorker@email.cz");
        $worker_3->setPassword("$2y$13\$z5D9vCv4NnwWx2bAmHDJx.tIJFsBX3o7/NWRZplQazMYprG7ux4xK"); //socialWorker
        $manager->persist($worker_3);
        ////////////////////////////////////////////

        /////////////// User - Vet /////////////////
        $vet = new User();
        $vet->setRole(1);
        $vet->setName("Milan Dobeš");
        $vet->setEmail("mdobes@email.cz");
        $vet->setPassword("$2y$13\$eDPFfU3E8tKSwJH67s3DpufCQRmQ4ASKIcuL6cndui3Q2WK0mdgTW"); //mdobes
        $manager->persist($vet);
        ////////////////////////////////////////////

        /////////////// User - Vet 2 ///////////////
        $vet_1 = new User();
        $vet_1->setRole(1);
        $vet_1->setName("Vet test");
        $vet_1->setEmail("vet@email.cz");
        $vet_1->setPassword("$2y$13\$aebNkqlyDTIRhUJJqhp8M.uON0qlosYi/UtAA6lH5HNR0bUmi6qw6"); //vet
        $manager->persist($vet_1);
        ////////////////////////////////////////////

        /////////////// User - Volunteer 1 /////////
        $volunteer_1 = new User();
        $volunteer_1->setRole(3);
        $volunteer_1->setName("Jan Král");
        $volunteer_1->setEmail("jkral@email.cz");
        $volunteer_1->setPassword("$2y$13\$pODybRGIYx8a0JIailO.eeiNq8owrdE.hMnoZJgs4A5.d99FECdbC"); //jkral
        $manager->persist($volunteer_1);
        ////////////////////////////////////////////

        /////////////// User - Volunteer 2 /////////
        $volunteer_2 = new User();
        $volunteer_2->setRole(3);
        $volunteer_2->setName("Martin Odehnal");
        $volunteer_2->setEmail("modehnal@email.cz");
        $volunteer_2->setPassword("$2y$13\$h.aGUOi24fwSbaMg5zl.FeO27QoIVWl5sn4Yi/XwyaJC2Cz7MfGtu"); //modehnal
        $manager->persist($volunteer_2);
        ////////////////////////////////////////////

        /////////////// User - Volunteer 3 /////////
        $volunteer_3 = new User();
        $volunteer_3->setRole(3);
        $volunteer_3->setName("Volunteer test");
        $volunteer_3->setEmail("volunteer@email.cz");
        $volunteer_3->setPassword("$2y$13\$S/QcrsxxQh3a9m6xTHkDG.dUVuMESUXCflWrAHf79dc8EbxQhflIS"); //volunteer
        $manager->persist($volunteer_3);
        ////////////////////////////////////////////

        /////////////// Dog - 20 dogs //////////////
        $names = array('Bůčo', 'Čůčo', 'Močo', 'Roko', 'Koko', 'Jumbo', 'Frederick', 'Jimbo', 'Tomíno', 'Bambíno');
        $chip_numbers = array('123456789123456', '987654321123456', '123654789963214');
        $desc = "Čůčo je krásný silný a zdravý kříženec labradora. Váží 33.5 kg, je mu 6 let. Temperament má jak na toto plemeno přiměřený, není to žádný blázen, co by musel běhat několik kilometrů denně. Stačí mu pravidelné procházky, nebo výběh na zahradě , na který byl zvyklý. Každý večer však ležel se svým páníčkem v posteli, takže to není žádný pes do boudy na hlídání, pouze jako životní parťák. K dětem ho raději nedoporučujeme, dle slov pozůstalých na ně nebyl zvyklý a nemá je úplně rád. U tak velkého psa se silným stiskem čelisti budeme tuto informaci respektovat. K dospělým lidem je hodný, nikdy nikoho nekousl. Samozřejmě dokáže za plotem nahánět hrůzu, ale když někoho pozná, miluje ho a na svoje lidi nedá dopustit. V útulku běžně chodí na procházky s cizími venčiteli, chválí si ho, chová slušně a umí chodit na vodítku, v útulku se vcelku rychle naučil nosit košík. Navíc je stoprocentně čistotný, vhodný do bytu. Nic neničí, je vyrovnaný a moc neštěká. Do adopce odejde kastrovaný očkovaný, čipovaný, odčervený, odblešený, na základě podpisu adopční smlouvy a uhrazení příspěvku na péči.";
        $photos = array("https://ggsc.s3.amazonaws.com/images/uploads/The_Science-Backed_Benefits_of_Being_a_Dog_Owner.jpg");

        for ($i = 0; $i < 20; $i++) {
            ${"dog_" . "$i"} = new Dog();
            ${"dog_" . "$i"}->setName($names[array_rand($names)]);
            ${"dog_" . "$i"}->setAge(mt_rand(1, 20));
            ${"dog_" . "$i"}->setPhoto($photos[array_rand($photos)]);
            ${"dog_" . "$i"}->setBreed(0);
            ${"dog_" . "$i"}->setChipNumber($chip_numbers[array_rand($chip_numbers)]);
            ${"dog_" . "$i"}->setIsCastrated(rand(0, 1) == 1);
            ${"dog_" . "$i"}->setDescription($desc);
            ${"dog_" . "$i"}->setInShelterBcs(1);
            ${"dog_" . "$i"}->setSex(rand(0, 1) == 1);
            ${"dog_" . "$i"}->setDewormed(rand(0, 1) == 1);
            ${"dog_" . "$i"}->setVaccinated(rand(0, 1) == 1);
            ${"dog_" . "$i"}->setPrice(mt_rand(500, 3500));
            $manager->persist(${"dog_" . "$i"});
        }
        ////////////////////////////////////////////

        /////////////// Calendar - Records /////////
        $record_1 = new CalendarRecord();

        $from = new DateTime("2022-11-27 15:00:00");
        $from->format("Y-m-d h:i:s");

        $to = new DateTime("2022-11-27 16:00:00");
        $to->format("Y-m-d h:i:s");

        $record_1->setTimestampFrom($from);
        $record_1->setDogId($dog_0);
        $record_1->setUserId($vet);
        $record_1->setTimestampTo($to);
        $record_1->setType(0); // medicalTreatment
        $record_1->setStatus(1);
        $manager->persist($record_1);

        $record_2 = new CalendarRecord();

        $from = new DateTime("2022-11-28 11:00:00");
        $from->format("Y-m-d h:i:s");

        $to = new DateTime("2022-11-28 12:00:00");
        $to->format("Y-m-d h:i:s");

        $record_2->setTimestampFrom($from);
        $record_2->setDogId($dog_1);
        $record_2->setUserId($volunteer_2);
        $record_2->setTimestampTo($to);
        $record_2->setType(1); // walking
        $record_2->setStatus(0); // undefined
        $manager->persist($record_2);

        $record_3 = new CalendarRecord();

        $from = new DateTime("2022-11-29 11:00:00");
        $from->format("Y-m-d h:i:s");

        $to = new DateTime("2022-11-29 12:00:00");
        $to->format("Y-m-d h:i:s");

        $record_3->setTimestampFrom($from);
        $record_3->setDogId($dog_2);
        $record_3->setUserId($volunteer_1);
        $record_3->setTimestampTo($to);
        $record_3->setType(1); // walking
        $record_3->setStatus(0); // undefined
        $manager->persist($record_3);

        $record_4 = new CalendarRecord();

        $from = new DateTime("2022-11-30 8:00:00");
        $from->format("Y-m-d h:i:s");

        $to = new DateTime("2022-11-30 10:00:00");
        $to->format("Y-m-d h:i:s");

        $record_4->setTimestampFrom($from);
        $record_4->setDogId($dog_3);
        $record_4->setUserId($volunteer_1);
        $record_4->setTimestampTo($to);
        $record_4->setType(1); // walking
        $record_4->setStatus(2); // zamítnuto
        $manager->persist($record_4);

        $record_5 = new CalendarRecord();

        $from = new DateTime("2022-11-29 11:00:00");
        $from->format("Y-m-d h:i:s");

        $to = new DateTime("2022-11-29 12:00:00");
        $to->format("Y-m-d h:i:s");

        $record_5->setTimestampFrom($from);
        $record_5->setDogId($dog_5);
        $record_5->setUserId($volunteer_1);
        $record_5->setTimestampTo($to);
        $record_5->setType(1); // walking
        $record_5->setStatus(0); // undefined
        $manager->persist($record_5);

        ////////////////////////////////////////////


        /////////////// MedicalRequest - Requests //
        $request_1 = new MedicalRequest();

        $desc_req = "Příliš živý a nezvladatelný.";

        $request_1->setSocialWorker($worker_1);
        $request_1->setVet($vet);
        $request_1->setDog($dog_1);
        $request_1->setType(2);
        $request_1->setDescription($desc_req);
        $request_1->setRequestDate($from);
        $request_1->setPriority(1);
        $request_1->setStatus(2);
        $manager->persist($request_1);

        $request_2 = new MedicalRequest();

        $desc_req = "Zabodlý trn v tlamě.";

        $request_2->setSocialWorker($worker_1);
        $request_2->setVet($vet);
        $request_2->setDog($dog_2);
        $request_2->setType(0);
        $request_2->setDescription($desc_req);
        $request_2->setRequestDate($from);
        $request_2->setPriority(1);
        $request_2->setStatus(1);
        $manager->persist($request_2);

        $request_3 = new MedicalRequest();

        $desc_req = "Kulhá na levou přední packu.";

        $request_3->setSocialWorker($worker_2);
        $request_3->setVet($vet_1);
        $request_3->setDog($dog_2);
        $request_3->setType(0);
        $request_3->setDescription($desc_req);
        $request_3->setRequestDate($from);
        $request_3->setPriority(2);
        $request_3->setStatus(1);
        $manager->persist($request_3);

        $request_4 = new MedicalRequest();

        $desc_req = "Kulhá na levou zadní packu.";

        $request_4->setSocialWorker($worker_3);
        $request_4->setVet($vet_1);
        $request_4->setDog($dog_2);
        $request_4->setType(0);
        $request_4->setDescription($desc_req);
        $request_4->setRequestDate($from);
        $request_4->setPriority(2);
        $request_4->setStatus(1);
        $manager->persist($request_4);
        ////////////////////////////////////////////

        /////////////// MedicalLog - Logs //////////
        $desc_log = "Kastrace proběhla v pořádku.";

        $log_1 = new MedicalLog();
        $log_1->setVet($vet);
        $log_1->setDog($dog_1);
        $log_1->setType(2);
        $log_1->setFinishedDate($from);
        $log_1->setDescription($desc_log);
        $manager->persist($log_1);

        $log_2 = new MedicalLog();
        $log_2->setVet($vet);
        $log_2->setDog($dog_1);
        $log_2->setType(2);
        $log_2->setFinishedDate($from);
        $log_2->setDescription($desc_log);
        $manager->persist($log_2);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);

        $log_3 = new MedicalLog();
        $log_3->setVet($vet);
        $log_3->setDog($dog_1);
        $log_3->setType(2);
        $log_3->setFinishedDate($from);
        $log_3->setDescription($desc_log);
        $manager->persist($log_3);
        ////////////////////////////////////////////

        $manager->flush();
    }
}
