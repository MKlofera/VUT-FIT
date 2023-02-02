# VUT-FIT ITU-ISS UTULEK 
- ITU 54/55
- IIS 20/30

# Getting Started
## 1.	Installation process

This installation is for UNIX

- install php:

```bash
$ sudo apt install php8.1
```

- install git and clone the repository

- install [nginx](https://www.nginx.com/) and configure it up to point it on public file of cloned repository

- install symphony-cli:

```bash
$ curl -1sLf 'https://dl.cloudsmith.io/public/symfony/stable/setup.deb.sh' | sudo -E bash 
$ sudo apt install symfony-cli
```
- install `composer` by steps on this [page](https://getcomposer.org/download/) 

- install dependency bundles:

```bash
# dependencies for symfony
$ composer install
```

- install `nodejs` and `npm` by steps on this [page](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)

- install dependency bundles:

```bash
# dependencies for React
$ npm install
# or
$ npm ci
```

- create `.env.local` file
- set up JWT token passphrase and create private and public key
```bash
$ php bin/console lexik:jwt:generate-keypair
```
- install mysql server step by step from this (linux) [page](https://learn.microsoft.com/en-us/sql/connect/php/installation-tutorial-linux-mac?view=sql-server-ver16) 
- set up database constants

## 2. Running the app

If you want to start a server, run this command:
```
$ symfony server:start
```

For immediate update when developing use this command:
```
$ npm run watch
```

And you are good to go

# Contribute

- [Symfony](https://symfony.com/)
- [React](https://reactjs.org/)
- [Zadání ITU](https://moodle.vut.cz/pluginfile.php/502738/mod_resource/content/0/Zad%C3%A1n%C3%AD%20projektu%20do%20p%C5%99edm%C4%9Btu%20ITU%202022_2023.pdf)
- [Zadání IIS](https://moodle.vut.cz/mod/page/view.php?id=238239)
