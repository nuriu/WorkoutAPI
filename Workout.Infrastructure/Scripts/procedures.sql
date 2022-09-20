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
    SELECT u.Id FROM users u
    WHERE u.username = username AND u.password = MD5(password);
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

CREATE PROCEDURE CreateMovement(IN name VARCHAR(32), IN description VARCHAR(128), IN muscle_group_id INT UNSIGNED, IN userId INT UNSIGNED)
BEGIN
    INSERT INTO movements (
        name,
        description,
        muscle_group_id,
        created_by,
        updated_by
    ) VALUES (
        name,
        description,
        muscle_group_id,
        userId,
        userId
    );

    SELECT
        m.id,
        m.name,
        m.description,
        m.muscle_group_id,
        m.created_at,
        m.created_by,
        m.updated_at,
        m.updated_by
    FROM movements m
    ORDER BY m.id DESC LIMIT 1;
END//

CREATE PROCEDURE GetMovementById(IN id INT UNSIGNED)
BEGIN
    SELECT
        m.id,
        m.name,
        m.description,
        m.muscle_group_id,
        m.created_at,
        m.created_by,
        m.updated_at,
        m.updated_by
    FROM movements m
    WHERE m.id = id;
END//

CREATE PROCEDURE GetMovementCount()
BEGIN
    SELECT COUNT(*) AS MovementCount FROM movements;
END//

CREATE PROCEDURE GetMovementsPaginated(IN pageIndex INT UNSIGNED, IN pageSize INT UNSIGNED)
BEGIN
    DECLARE skipCount INT;
    SET skipCount = (pageIndex - 1) * pageSize;
    SELECT
        m.id,
        m.name,
        m.description,
        m.muscle_group_id,
        m.created_at,
        m.created_by,
        m.updated_at,
        m.updated_by
    FROM movements m
    LIMIT pageSize
    OFFSET skipCount;
END//

CREATE PROCEDURE DeleteMovementById(IN id INT UNSIGNED)
BEGIN
    DELETE FROM movements m WHERE m.id = id;
    SELECT ROW_COUNT() AS DeletedRecordCount;
END//

CREATE PROCEDURE CreateWorkout(IN name VARCHAR(32), IN description VARCHAR(128), IN duration SMALLINT UNSIGNED, IN difficulty_level_id INT UNSIGNED, IN userId INT UNSIGNED)
BEGIN
    INSERT INTO workouts (
        name,
        description,
        duration,
        difficulty_level_id,
        created_by,
        updated_by
    ) VALUES (
        name,
        description,
        duration,
        difficulty_level_id,
        userId,
        userId
    );

    SELECT
        w.id,
        w.name,
        w.description,
        w.duration,
        w.difficulty_level_id,
        w.created_at,
        w.created_by,
        w.updated_at,
        w.updated_by
    FROM workouts w
    ORDER BY w.id DESC LIMIT 1;
END//

CREATE PROCEDURE GetWorkoutById(IN id INT UNSIGNED)
BEGIN
    SELECT
        w.id,
        w.name,
        w.description,
        w.duration,
        w.difficulty_level_id,
        w.created_at,
        w.created_by,
        w.updated_at,
        w.updated_by
    FROM workouts w
    WHERE w.id = id;
END//

CREATE PROCEDURE GetWorkoutCount()
BEGIN
    SELECT COUNT(*) AS WorkoutCount FROM workouts;
END//

CREATE PROCEDURE GetWorkoutsPaginated(IN pageIndex INT UNSIGNED, IN pageSize INT UNSIGNED)
BEGIN
    DECLARE skipCount INT;
    SET skipCount = (pageIndex - 1) * pageSize;
    SELECT
        w.id,
        w.name,
        w.description,
        w.duration,
        w.difficulty_level_id,
        w.created_at,
        w.created_by,
        w.updated_at,
        w.updated_by
    FROM workouts w
    LIMIT pageSize
    OFFSET skipCount;
END//

CREATE PROCEDURE DeleteWorkoutById(IN id INT UNSIGNED)
BEGIN
    DELETE FROM workouts w WHERE w.id = id;
    SELECT ROW_COUNT() AS DeletedRecordCount;
END//

DELIMITER ;
