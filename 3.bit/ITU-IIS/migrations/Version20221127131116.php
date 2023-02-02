<?php

declare(strict_types=1);

namespace DoctrineMigrations;

use Doctrine\DBAL\Schema\Schema;
use Doctrine\Migrations\AbstractMigration;

/**
 * Auto-generated Migration: Please modify to your needs!
 */
final class Version20221127131116 extends AbstractMigration
{
    public function getDescription(): string
    {
        return '';
    }

    public function up(Schema $schema): void
    {
        // this up() migration is auto-generated, please modify it to your needs
        $this->addSql('ALTER TABLE calendar_record ALTER COLUMN timestamp_from DATE NOT NULL');
        $this->addSql('ALTER TABLE calendar_record ALTER COLUMN timestamp_to DATE NOT NULL');
        $this->addSql('EXEC sp_dropextendedproperty N\'MS_Description\', N\'SCHEMA\', \'dbo\', N\'TABLE\', \'calendar_record\', N\'COLUMN\', timestamp_from');
        $this->addSql('EXEC sp_dropextendedproperty N\'MS_Description\', N\'SCHEMA\', \'dbo\', N\'TABLE\', \'calendar_record\', N\'COLUMN\', timestamp_to');
    }

    public function down(Schema $schema): void
    {
        // this down() migration is auto-generated, please modify it to your needs
        $this->addSql('CREATE SCHEMA db_accessadmin');
        $this->addSql('CREATE SCHEMA db_backupoperator');
        $this->addSql('CREATE SCHEMA db_datareader');
        $this->addSql('CREATE SCHEMA db_datawriter');
        $this->addSql('CREATE SCHEMA db_ddladmin');
        $this->addSql('CREATE SCHEMA db_denydatareader');
        $this->addSql('CREATE SCHEMA db_denydatawriter');
        $this->addSql('CREATE SCHEMA db_owner');
        $this->addSql('CREATE SCHEMA db_securityadmin');
        $this->addSql('CREATE SCHEMA dbo');
        $this->addSql('ALTER TABLE calendar_record ALTER COLUMN timestamp_from DATETIME2(6) NOT NULL');
        $this->addSql('ALTER TABLE calendar_record ALTER COLUMN timestamp_to DATETIME2(6) NOT NULL');
        $this->addSql('EXEC sp_addextendedproperty N\'MS_Description\', N\'(DC2Type:datetime_immutable)\', N\'SCHEMA\', \'dbo\', N\'TABLE\', \'calendar_record\', N\'COLUMN\', timestamp_from');
        $this->addSql('EXEC sp_addextendedproperty N\'MS_Description\', N\'(DC2Type:datetime_immutable)\', N\'SCHEMA\', \'dbo\', N\'TABLE\', \'calendar_record\', N\'COLUMN\', timestamp_to');
    }
}
