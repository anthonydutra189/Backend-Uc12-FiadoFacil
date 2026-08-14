# COMPANY





## TABELA

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
    


## FILTROS