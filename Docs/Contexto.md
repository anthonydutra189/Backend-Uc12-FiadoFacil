# Fiado Facil 

O Fiado Fácil é um sistema que permite que os comerciantes cadastrem seus clientes e registrem os fiados deles. O sistema permite que os comerciantes cadastrem seus clientes e registrem os fiados deles.

## Integrantes do grupo
- Antony
- Milena
- Wanderson
- Felipe

### Divisão de tarefas
Classes que cada um tem que criar e fazer funcionar:
- `User`: Felipe
- `Company`: Wanderson
- `Product`: Milena
- `Payment`: Antony
  - `_product_payment`

## CRUD
Operações do CRUD (inserir, ler, atualizar, deletar) para cada tela:
- `User`: inserir, ler, atualizar, deletar
  - O deletar deve ser em formato de cascata (deletar todas as informações atreladas a ele)
- `Company`: inserir, ler, atualizar
  - Não tem delete
- `Product`: inserir, ler, atualizar, deletar
  - Deletar em cascata
- `Payment`: inserir, ler, deletar
  - Não tem atualizar
  - `_product_payment`: inserir, ler

## Banco de dados

- **users**: usuários do sistema
	- **Customer:** dono do estabelecimento
	- **Admin:** dono do sistema
	- **Client:** cliente que está devendo para o Customer (dono do estabelecimento)
- **companies**: empresa do usuário que usa o sistema
	- Chave estrangeira com `user`
- **products**: produtos que pertencem a uma empresa
	- Chave estrangeira com `companies`
- **payments**: pagamentos do usuário para com a empresa
	- Chave estrangeira do `user` do tipo *Client*
	- Chave estrangeira com `companies`
- **_produto_payment**: tabela somente de chaves estrangeiras que liga o produto e o pagamento


### Schema

- Usuários
    
    CREATE TABLE `users` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(191) NOT NULL,
        `type` varchar(191) NOT NULL,
        `email` varchar(191) NOT NULL,
        `senha` varchar(191) NOT NULL,
        `createAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
         `updateAt` datetime(3) NOT NULL,
        PRIMARY KEY (`id`)
        ) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
    


- Company

    CREATE TABLE `companies` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(191) NOT NULL,
        `category` varchar(191) NOT NULL,
        `cnpj` varchar(191) NOT NULL,
        `places` varchar(191) NOT NULL,
        `zip_code` varchar(191) NOT NULL,
        `addrres` varchar(191) NOT NULL,
        `phone` varchar(191) NOT NULL,
        `user_id` int(11) NOT NULL,
        `logoUrl` varchar(191) DEFAULT NULL,
        `deletedAt` datetime(3) DEFAULT NULL,
        `createAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
        `updateAt` datetime(3) NOT NULL,
        PRIMARY KEY (`id`),
        KEY `companies_user_id_fkey` (`user_id`),
        CONSTRAINT `companies_user_id_fkey` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON UPDATE CASCADE
        ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci    
    


- Payment

    CREATE TABLE `payments` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `value` double NOT NULL,
        `method` varchar(191) NOT NULL,
        `to_date` datetime(3) NOT NULL,
        `due_date` datetime(3) NOT NULL,
        `user_id` int(11) NOT NULL,
        `company_id` int(11) NOT NULL,
        `createAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
        `updateAt` datetime(3) NOT NULL,
        PRIMARY KEY (`id`),
        KEY `payments_user_id_fkey` (`user_id`),
        KEY `payments_company_id_fkey` (`company_id`),
        CONSTRAINT `payments_company_id_fkey` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`) ON UPDATE CASCADE,
        CONSTRAINT `payments_user_id_fkey` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON UPDATE CASCADE
        ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
    

- Product

     CREATE TABLE `products` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(191) NOT NULL,
        `type` varchar(191) NOT NULL,
        `value` double NOT NULL,
        `description` varchar(191) NOT NULL,
        `url_img` varchar(191) NOT NULL,
        `company_id` int(11) NOT NULL,
        `createAt` datetime(3) NOT NULL DEFAULT current_timestamp(3),
        `updateAt` datetime(3) NOT NULL,
        PRIMARY KEY (`id`),
        KEY `products_company_id_fkey` (`company_id`),
        CONSTRAINT `products_company_id_fkey` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`) ON UPDATE CASCADE
        ) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
    

### SCHEMA PRISMA

me de o sql para criar as tabelas no mySQL deste schema aqui:
// This is your Prisma schema file,
// learn more about it in the docs: https://pris.ly/d/prisma-schema

generator client {
  provider = "prisma-client-js"
  
}

datasource db {
  provider = "mysql"
  url      = env("DATABASE_URL")
}

model Company {
  id        Int       @id @default(autoincrement())

  name      String    
  category  String
  cnpj      String
  places    String
  zip_code  String
  addrres   String
  phone     String     

  userId     Int        @map("user_id")
  user       User       @relation(fields: [userId], references:[id])
  
  products  Product[]    
  payments  Payment[]

  logoUrl   String?
  deletedAt DateTime?

  createAt  DateTime    @default(now())
  updateAt  DateTime    @updatedAt
  
  @@map("companies")
}





model Product {
  id                  Int               @id @default(autoincrement())

  name                String
  type                String 
  value               Float
  description         String 
  url_img             String
  
  companyId           Int               @map("company_id")
  companies           Company           @relation(fields: [companyId], references:[id])
  
  payments          Payment[]           @relation("product_payment")

  createAt  DateTime    @default(now())
  updateAt  DateTime    @updatedAt

  @@map("products")
}

model Payment {
  id              Int                   @id   @default(autoincrement())
  
  value           Float
  method   String   
  toDate            DateTime                    @map("to_date")
  dueDate           DateTime                    @map("due_date") 
  
  userId            Int                         @map("user_id")
  user              User                        @relation(fields: [userId], references: [id])
  
  companyId       Int                           @map("company_id")
  company         Company                       @relation(fields: [companyId], references:[id])

  products          Product[]                   @relation("product_payment")

  createAt          DateTime                    @default(now())
  updateAt          DateTime                    @updatedAt
  
  @@map("payments")
}


model User{
  id          Int         @id   @default(autoincrement())

  name        String
  type        String
  email       String
  senha       String

  companies  Company[]
  payments  Payment[]
  
  createAt  DateTime    @default(now())
  updateAt  DateTime    @updatedAt

  @@map("users") 
}

## REGRAS

 - User
    - hieraquia 