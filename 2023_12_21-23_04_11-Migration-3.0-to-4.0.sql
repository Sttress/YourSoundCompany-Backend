﻿START TRANSACTION;

CREATE TABLE "Category" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    CONSTRAINT "PK_Category" PRIMARY KEY ("Id")
);

CREATE TABLE "Product" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    "Description" text NULL,
    "CategoryId" bigint NULL,
    "Price" bigint NULL,
    "Amount" bigint NULL,
    CONSTRAINT "PK_Product" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Product_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Product_CategoryId" ON "Product" ("CategoryId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231222020103_4.0', '7.0.14');

COMMIT;
