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

    SELECT
        u.id,
        u.username,
        u.password,
        u.created_at,
        u.updated_at
    FROM users u
    WHERE u.username = username AND u.password = MD5(password);
END//

CREATE PROCEDURE IsUserExists(IN username VARCHAR(16), IN password VARCHAR(32))
BEGIN
    SELECT EXISTS (
        SELECT * FROM users u
        WHERE u.username = username AND u.password = MD5(password)
    ) AS UserExists;
END//

CREATE PROCEDURE GetUserById(IN id INT UNSIGNED)
BEGIN
    SELECT
        u.id,
        u.username,
        u.password,
        u.created_at,
        u.updated_at
    FROM users u
    WHERE u.id = id;
END//

CREATE PROCEDURE DeleteUserById(IN id INT UNSIGNED)
BEGIN
    DELETE FROM users u WHERE u.id = id;
    SELECT ROW_COUNT() AS DeletedRecordCount;
END//

CREATE PROCEDURE GetUserCount()
BEGIN
    SELECT COUNT(*) AS UserCount FROM users;
END//

CREATE PROCEDURE GetUsersPaginated(IN pageIndex INT UNSIGNED, IN pageSize INT UNSIGNED)
BEGIN
    DECLARE skipCount INT;
    SET skipCount = (pageIndex - 1) * pageSize;
    SELECT
        u.id,
        u.username,
        u.password,
        u.created_at,
        u.updated_at
    FROM users u
    LIMIT pageSize
    OFFSET skipCount;
END//

CREATE PROCEDURE CreateDifficultyLevel(IN name VARCHAR(32), IN description VARCHAR(128), IN userId INT UNSIGNED)
BEGIN
    INSERT INTO difficulty_levels (
        name,
        description,
        created_by,
        updated_by
    ) VALUES (
        name,
        description,
        userId,
        userId
    );

    SELECT
        dl.id,
        dl.name,
        dl.description,
        dl.created_at,
        dl.created_by,
        dl.updated_at,
        dl.updated_by
    FROM difficulty_levels dl
    ORDER BY dl.id DESC LIMIT 1;
END//

CREATE PROCEDURE GetDifficultyLevelById(IN id INT UNSIGNED)
BEGIN
    SELECT
        dl.id,
        dl.name,
        dl.description,
        dl.created_at,
        dl.created_by,
        dl.updated_at,
        dl.updated_by
    FROM difficulty_levels dl
    WHERE dl.id = id;
END//

CREATE PROCEDURE GetDifficultyLevelCount()
BEGIN
    SELECT COUNT(*) AS DifficultyLevelCount FROM difficulty_levels;
END//

CREATE PROCEDURE GetDifficultyLevelsPaginated(IN pageIndex INT UNSIGNED, IN pageSize INT UNSIGNED)
BEGIN
    DECLARE skipCount INT;
    SET skipCount = (pageIndex - 1) * pageSize;
    SELECT
        dl.id,
        dl.name,
        dl.description,
        dl.created_at,
        dl.created_by,
        dl.updated_at,
        dl.updated_by
    FROM difficulty_levels dl
    LIMIT pageSize
    OFFSET skipCount;
END//

CREATE PROCEDURE DeleteDifficultyLevelById(IN id INT UNSIGNED)
BEGIN
    DELETE FROM difficulty_levels dl WHERE dl.id = id;
    SELECT ROW_COUNT() AS DeletedRecordCount;
END//

CREATE PROCEDURE CreateMuscleGroup(IN name VARCHAR(32), IN description VARCHAR(128), IN userId INT UNSIGNED)
BEGIN
    INSERT INTO muscle_groups (
        name,
        description,
        created_by,
        updated_by
    ) VALUES (
        name,
        description,
        userId,
        userId
    );

    SELECT
        mg.id,
        mg.name,
        mg.description,
        mg.created_at,
        mg.created_by,
        mg.updated_at,
        mg.updated_by
    FROM muscle_groups mg
    ORDER BY mg.id DESC LIMIT 1;
END//

CREATE PROCEDURE GetMuscleGroupById(IN id INT UNSIGNED)
BEGIN
    SELECT
        mg.id,
        mg.name,
        mg.description,
        mg.created_at,
        mg.created_by,
        mg.updated_at,
        mg.updated_by
    FROM muscle_groups mg
    WHERE mg.id = id;
END//

CREATE PROCEDURE GetMuscleGroupCount()
BEGIN
    SELECT COUNT(*) AS MuscleGroupCount FROM muscle_groups;
END//

CREATE PROCEDURE GetMuscleGroupsPaginated(IN pageIndex INT UNSIGNED, IN pageSize INT UNSIGNED)
BEGIN
    DECLARE skipCount INT;
    SET skipCount = (pageIndex - 1) * pageSize;
    SELECT
        mg.id,
        mg.name,
        mg.description,
        mg.created_at,
        mg.created_by,
        mg.updated_at,
        mg.updated_by
    FROM muscle_groups mg
    LIMIT pageSize
    OFFSET skipCount;
END//

CREATE PROCEDURE DeleteMuscleGroupById(IN id INT UNSIGNED)
BEGIN
    DELETE FROM muscle_groups mg WHERE mg.id = id;
    SELECT ROW_COUNT() AS DeletedRecordCount;
END//

DELIMITER ;
