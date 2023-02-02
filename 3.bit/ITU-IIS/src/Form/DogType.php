<?php

namespace App\Form;

use App\Entity\Dog;
use Symfony\Component\Form\AbstractType;
use Symfony\Component\Form\FormBuilderInterface;
use Symfony\Component\OptionsResolver\OptionsResolver;

class DogType extends AbstractType
{
    public function buildForm(FormBuilderInterface $builder, array $options): void
    {
        $builder
            ->add('Name')
            ->add('Age')
            ->add('Photo')
            ->add('Breed')
            ->add('ChipNumber')
            ->add('IsCastrated')
            ->add('Description')
            ->add('InShelterBcs')
            ->add('Sex')
            ->add('Dewormed')
            ->add('Vaccinated')
            ->add('Price')
            ->add('CalendarRecords')
            ->add('MedicalLogs')
            ->add('MedicalRequests')
        ;
    }

    public function configureOptions(OptionsResolver $resolver): void
    {
        $resolver->setDefaults([
            'data_class' => Dog::class,
            'validation_groups' => false
        ]);
    }
}
