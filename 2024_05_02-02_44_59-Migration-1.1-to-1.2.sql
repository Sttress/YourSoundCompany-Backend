START TRANSACTION;

ALTER TABLE "User" ADD "Active" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240502054409_1.2', '7.0.14');

COMMIT;

