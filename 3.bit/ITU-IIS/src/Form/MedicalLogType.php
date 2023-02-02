<?php

namespace App\Form;

use App\Entity\MedicalLog;
use Symfony\Component\Form\AbstractType;
use Symfony\Component\Form\FormBuilderInterface;
use Symfony\Component\OptionsResolver\OptionsResolver;

class MedicalLogType extends AbstractType
{
    public function buildForm(FormBuilderInterface $builder, array $options): void
    {
        $builder
            ->add('VetId')
            ->add('DogId')
            ->add('Type')
            ->add('FinishedDate')
            ->add('Description')
            ->add('vet')
            ->add('Dog')
        ;
    }

    public function configureOptions(OptionsResolver $resolver): void
    {
        $resolver->setDefaults([
            'data_class' => MedicalLog::class,
        ]);
    }
}
