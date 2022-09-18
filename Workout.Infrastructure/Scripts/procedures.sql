USE workout;

DELIMITER //

CREATE PROCEDURE CreateUser(username VARCHAR(16), password VARCHAR(32))
BEGIN
    INSERT INTO users (
        username,
        password
    ) VALUES (
        username,
        MD5(password)
    );
END//

CREATE PROCEDURE GetUserByUsername(username VARCHAR(16))
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
