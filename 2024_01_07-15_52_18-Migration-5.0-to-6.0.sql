START TRANSACTION;

ALTER TABLE "Product" DROP COLUMN "Amount";

ALTER TABLE "Product" ADD "Active" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE "Product" ADD "UserId" bigint NOT NULL DEFAULT 0;

CREATE INDEX "IX_Product_UserId" ON "Product" ("UserId");

ALTER TABLE "Product" ADD CONSTRAINT "FK_Product_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240107184853_6.0', '7.0.14');

COMMIT;

