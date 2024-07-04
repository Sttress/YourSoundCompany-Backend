START TRANSACTION;

ALTER TABLE "User" DROP COLUMN "ClientId";

ALTER TABLE "User" RENAME COLUMN "Token" TO "UrlImageProfile";

ALTER TABLE "User" RENAME COLUMN "RefreshToken" TO "NumberPhone";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240704215403_1.3', '7.0.14');

COMMIT;

