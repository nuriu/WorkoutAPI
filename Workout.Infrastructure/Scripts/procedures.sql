USE workout;

DELIMITER //

CREATE PROCEDURE CreateUser(IN username VARCHAR(16), IN password VARCHAR(32))
BEGIN
    INSERT INTO users (
        username,
        password
    ) VALUES (
        username,
        MD5(password)
    );
END//

CREATE PROCEDURE IsUserExists(IN username VARCHAR(16), IN password VARCHAR(32))
BEGIN
    SELECT EXISTS (
        SELECT * FROM users u
        WHERE u.username = username AND u.password = MD5(password)
    ) AS UserExists;
END//

CREATE PROCEDURE GetUserByUsername(IN username VARCHAR(16))
BEGIN
    SELECT
        id,
        username,
        password,
        created_at,
        updated_at
    FROM users u
    WHERE u.username = username;
END//

DELIMITER ;
