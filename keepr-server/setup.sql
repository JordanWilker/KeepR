 
--  CREATE TABLE profiles
--     (
--         id VARCHAR(255) NOT NULL,
--         email VARCHAR(255) NOT NULL,
--         name VARCHAR(255),
--         picture VARCHAR(255),
--         creator VARCHAR(255),
--         PRIMARY KEY (id)
--     );

-- CREATE TABLE vault
--     (
--         id INT AUTO_INCREMENT,
--         creatorId VARCHAR(255),
--         name VARCHAR(255),
--         description VARCHAR(255),
--         isPrivate TINYINT,
--         PRIMARY KEY(id),

--         FOREIGN KEY(creatorId)
--             REFERENCES profiles(id)
--             ON DELETE CASCADE
--     );

-- CREATE TABLE keep
--     (
--         id INT AUTO_INCREMENT,
--         creatorId VARCHAR(255),
--         name VARCHAR(255),
--         description VARCHAR(255),
--         img VARCHAR(255),
--         views INT,
--         shares INT,
--         keeps INT, 
--         PRIMARY KEY(id),
--         FOREIGN KEY(creatorId)
--             REFERENCES profiles(id)
--             ON DELETE CASCADE
--     );
-- DROP TABLE

--  CREATE TABLE vaultkeep
-- (
--   id INT NOT NULL AUTO_INCREMENT, 
--   keepId INT,
--   vaultId INT,
--   creatorId VARCHAR(255),
--   PRIMARY KEY (id),

--    FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (vaultId)
--    REFERENCES vault (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (keepId)
--    REFERENCES keep (id)
--    ON DELETE CASCADE

-- )

-- DROP TABLE keep
