START TRANSACTION;

ALTER TABLE "Product" DROP CONSTRAINT "FK_Product_Category_CategoryId";

DROP INDEX "IX_Product_CategoryId";

ALTER TABLE "Product" DROP COLUMN "CategoryId";

ALTER TABLE "Product" ALTER COLUMN "Id" TYPE bigint;

ALTER TABLE "Category" ALTER COLUMN "Id" TYPE bigint;

ALTER TABLE "Category" ADD "Active" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE "Category" ADD "UserId" bigint NULL;

CREATE INDEX "IX_Product_CategoryId" ON "Product" ("CategoryId");

CREATE INDEX "IX_Category_UserId" ON "Category" ("UserId");

ALTER TABLE "Category" ADD CONSTRAINT "FK_Category_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

ALTER TABLE "Product" ADD CONSTRAINT "FK_Product_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240105123407_5.0', '7.0.14');

COMMIT;

