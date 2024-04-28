START TRANSACTION;

ALTER TABLE "User" ADD "ClientId" text NULL;

ALTER TABLE "User" ADD "RefreshToken" text NULL;

ALTER TABLE "User" ADD "Token" text NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240428050757_1.1', '7.0.14');

COMMIT;

