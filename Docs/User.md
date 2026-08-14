# User



## TABELA
    
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


## FILTROS