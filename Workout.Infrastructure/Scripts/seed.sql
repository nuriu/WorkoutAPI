DROP PROCEDURE IF EXISTS SeedMovementTable;
DROP PROCEDURE IF EXISTS SeedWorkoutTable;
DROP PROCEDURE IF EXISTS SeedWorkoutMovementsTable;

DELIMITER //

CREATE PROCEDURE SeedMovementTable()
BEGIN
    DECLARE i int DEFAULT 1;
    DROP TEMPORARY TABLE IF EXISTS t;
    CREATE TEMPORARY TABLE t SELECT * FROM movements LIMIT 0;

    SELECT id INTO @admin_id FROM users WHERE username = 'admin' LIMIT 1;
    SELECT COUNT(*) INTO @muscle_groups_count FROM muscle_groups;

    WHILE i <= 1000000 DO
        SELECT FLOOR(RAND() * (@muscle_groups_count - 2) + 1) INTO @mg_id;
        INSERT INTO t(name, description, muscle_group_id, created_by, updated_by)
        VALUES (CONCAT("Movement", '-', i), CONCAT("Movement Description", '-', i), @mg_id, @admin_id, @admin_id);

        SET i = i + 1;
    END WHILE;

    INSERT INTO movements SELECT * FROM t;
    DROP TEMPORARY TABLE t;
END//

CREATE PROCEDURE SeedWorkoutTable()
BEGIN
    DECLARE i int DEFAULT 1;
    DROP TEMPORARY TABLE IF EXISTS t;
    CREATE TEMPORARY TABLE t SELECT * FROM workouts LIMIT 0;

    SELECT id INTO @admin_id FROM users WHERE username = 'admin' LIMIT 1;

    WHILE i <= 10000 DO
        SELECT FLOOR(RAND() * (1800 - 60 + 1) + 60) INTO @duration;
        SELECT id INTO @dl_id FROM difficulty_levels ORDER BY RAND() LIMIT 1;

        INSERT INTO t(name, description, duration, difficulty_level_id, created_by, updated_by)
        VALUES (CONCAT("Workout", '-', i), CONCAT("Workout Description", '-', i), @duration, @dl_id, @admin_id, @admin_id);

        SET i = i + 1;
    END WHILE;

    INSERT INTO workouts SELECT * FROM t;
    DROP TEMPORARY TABLE t;
END//

CREATE PROCEDURE SeedWorkoutMovementsTable()
BEGIN
    DECLARE i int DEFAULT 1;

    DROP TEMPORARY TABLE IF EXISTS t;
    CREATE TEMPORARY TABLE t SELECT * FROM workout_movements LIMIT 0;

    SELECT COUNT(*) INTO @movements_count FROM movements;
    SELECT COUNT(*) INTO @workouts_count FROM workouts;

    WHILE i <= @movements_count DO
        SELECT FLOOR(RAND() * (@workouts_count - 2) + 1) INTO @w_id;

        INSERT INTO t(workout_id, movement_id) VALUES (@w_id, i);

        SET i = i + 1;
    END WHILE;

    INSERT INTO workout_movements SELECT * FROM t;
    DROP TEMPORARY TABLE t;
END//

DELIMITER ;

CALL SeedMovementTable();
CALL SeedWorkoutTable();
CALL SeedWorkoutMovementsTable();

DROP PROCEDURE IF EXISTS SeedMovementTable;
DROP PROCEDURE IF EXISTS SeedWorkoutTable;
DROP PROCEDURE IF EXISTS SeedWorkoutMovementsTable;
