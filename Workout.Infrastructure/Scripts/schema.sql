USE workout;

CREATE TABLE IF NOT EXISTS users (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(16) NOT NULL UNIQUE,
    password VARCHAR(32) NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    updated_at TIMESTAMP DEFAULT NOW() ON UPDATE NOW()
);

CREATE TABLE IF NOT EXISTS muscle_groups (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(64) NOT NULL,
    description VARCHAR(512),
    created_at TIMESTAMP DEFAULT NOW(),
    created_by INT UNSIGNED NOT NULL,
    updated_at TIMESTAMP DEFAULT NOW() ON UPDATE NOW(),
    updated_by INT UNSIGNED NOT NULL,
    CONSTRAINT fk_muscle_groups_creator FOREIGN KEY (created_by) REFERENCES users(id),
    CONSTRAINT fk_muscle_groups_updater FOREIGN KEY (updated_by) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS movements (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(64) NOT NULL,
    description VARCHAR(1024),
    muscle_group_id INT UNSIGNED NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    created_by INT UNSIGNED NOT NULL,
    updated_at TIMESTAMP DEFAULT NOW() ON UPDATE NOW(),
    updated_by INT UNSIGNED NOT NULL,
    CONSTRAINT fk_movements_muscle_group FOREIGN KEY (muscle_group_id) REFERENCES muscle_groups(id),
    CONSTRAINT fk_movements_creator FOREIGN KEY (created_by) REFERENCES users(id),
    CONSTRAINT fk_movements_updater FOREIGN KEY (updated_by) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS difficulty_levels (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(32) NOT NULL,
    description VARCHAR(128),
    created_at TIMESTAMP DEFAULT NOW(),
    created_by INT UNSIGNED NOT NULL,
    updated_at TIMESTAMP DEFAULT NOW() ON UPDATE NOW(),
    updated_by INT UNSIGNED NOT NULL,
    CONSTRAINT fk_difficulty_levels_creator FOREIGN KEY (created_by) REFERENCES users(id),
    CONSTRAINT fk_difficulty_levels_updater FOREIGN KEY (updated_by) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS workouts (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(64) NOT NULL,
    description VARCHAR(2048),
    duration SMALLINT UNSIGNED,
    difficulty_level_id INT UNSIGNED,
    created_at TIMESTAMP DEFAULT NOW(),
    created_by INT UNSIGNED NOT NULL,
    updated_at TIMESTAMP DEFAULT NOW() ON UPDATE NOW(),
    updated_by INT UNSIGNED NOT NULL,
    CONSTRAINT fk_workouts_difficulty_level FOREIGN KEY (difficulty_level_id) REFERENCES difficulty_levels(id),
    CONSTRAINT fk_workouts_creator FOREIGN KEY (created_by) REFERENCES users(id),
    CONSTRAINT fk_workouts_updater FOREIGN KEY (updated_by) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS workout_movements (
    workout_id INT UNSIGNED NOT NULL,
    movement_id INT UNSIGNED NOT NULL,
    CONSTRAINT fk_workout_movements_workout FOREIGN KEY (workout_id) REFERENCES workouts(id) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_workout_movements_movement FOREIGN KEY (movement_id) REFERENCES movements(id) ON UPDATE CASCADE ON DELETE CASCADE
);
