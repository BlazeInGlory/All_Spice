-- Active: 1687988652544@@SG-codeworks-7659-mysql-master.servers.mongodirector.com@3306@sandbox

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    recipes(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(255) NOT NULL,
        instructions TEXT NOT NULL,
        img VARCHAR(255) NOT NULL,
        category VARCHAR(100) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        recipeId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

-- DROP TABLE recipes;

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        "Chicken Flautas",
        "Boil the chicken until done, then shred the chicken. Throw it into a frying pan, add a generous amount of Chili Powder, until brown. Now once you've cooked some of the water out, add your v8 (spicy or original). Once done, wrap in corn tortillas, and fry tortillas until golden brown and crispy. Add toppings, and enjoy.",
        "https://cdn.apartmenttherapy.info/image/upload/f_auto,q_auto:eco,c_fill,g_auto,w_375,h_250/k%2FPhoto%2FRecipes%2F2019-06-how-to-make-the-best-chicken-flautas%2FHow-to-Make-Best-Chicken-Flautas_094",
        "Chicken",
        '646e6a3ec20f0b325ef61c98'
    );

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        archived,
        category,
        creatorId
    )
VALUES (
        "Mashed Taters",
        "Boil em', mash em', stick em' in a stew!",
        "https://m.media-amazon.com/images/I/81VJmBXpp2L._AC_SL1500_.jpg",
        "Vegetables",
        "646e6a3ec20f0b325ef61c98"
    );

DELETE FROM accounts WHERE id = '646e6a3ec20f0b325ef61c9';

SELECT *
FROM recipes
    JOIN accounts ON recipe.creatorId = accounts.id
WHERE
    accounts.id = '646e6a3ec20f0b325ef61c9'
SELECT rec.*, creator.*
FROM recipes rec
    JOIN accounts creator ON rec.creatorId = creator.id
WHERE
    creator.id = '646e6a3ec20f0b325ef61c9';

SELECT rec.*, creator.*
FROM recipes rec
    JOIN accounts creator ON rec.creatorId = creator.id
WHERE rec.id = 22;

SELECT title,
FROM recipes
    JOIN accounts ON recipe.creatorId = accounts.id
WHERE
    accounts.id = '646e6a3ec20f0b325ef61c9';