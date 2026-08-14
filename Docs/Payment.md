# Payment



## TABELA

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
    
## FILTROS