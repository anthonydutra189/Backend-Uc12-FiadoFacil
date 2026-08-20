# Product





## TABELA

CREATE TABLE `products` ()
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

## FILTROS