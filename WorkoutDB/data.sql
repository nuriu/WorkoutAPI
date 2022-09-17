USE workout;

CALL CreateUser('admin', 'admin');
CALL CreateUser('user', 'user');

SELECT id INTO @admin_id FROM users WHERE username = 'admin' LIMIT 1;

INSERT INTO muscle_groups (name, created_by, updated_by)
VALUES
    ('Neck', @admin_id, @admin_id),
    ('Chest', @admin_id, @admin_id),
    ('Shoulders', @admin_id, @admin_id),
    ('Biceps', @admin_id, @admin_id),
    ('Forearms', @admin_id, @admin_id),
    ('Abs', @admin_id, @admin_id),
    ('Thighs', @admin_id, @admin_id),
    ('Calves', @admin_id, @admin_id),
    ('Back', @admin_id, @admin_id),
    ('Triceps', @admin_id, @admin_id),
    ('Glutes', @admin_id, @admin_id),
    ('Hamstrings', @admin_id, @admin_id);

INSERT INTO difficulty_levels (name, created_by, updated_by)
VALUES
    ('Easy', @admin_id, @admin_id),
    ('Normal', @admin_id, @admin_id),
    ('Hard', @admin_id, @admin_id);
