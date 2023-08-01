CREATE TABLE DC_INV_NUMBERING (
    ID varchar(15) PRIMARY KEY,
    Prefix varchar(8) NULL,
    Next_Number decimal(10, 0) NULL,
    Suffix varchar(8) NULL,
    Digits int NULL,
    Restart bit NULL
);

CREATE TABLE DC_INV_OFFICE(
	ID varchar(15) PRIMARY KEY,
	Name varchar(100),
	Address varchar(100),
	Phone varchar(20)
);

create table DC_INV_WAREHOUSE(
	ID varchar(15) PRIMARY KEY,
	Description varchar(100),
	ID_Office varchar(15),
	FOREIGN KEY (ID_Office) REFERENCES DC_INV_OFFICE(ID)
);

create table DC_INV_PRODUCT(
	ID varchar(15) PRIMARY KEY,
	Barcode int,
	Name varchar(100),
	Description varchar(100),
	Price decimal(7,2),
	Cost decimal(7,2)
);

create table DC_INV_WAREHOUSE_INVENTORY (
	ID varchar(15) PRIMARY KEY,
	ID_Warehouse varchar(15),
	ID_Product varchar(15),
	Amount int,
	FOREIGN KEY (ID_Warehouse) REFERENCES DC_INV_WAREHOUSE(ID),
	FOREIGN KEY (ID_Product) REFERENCES DC_INV_PRODUCT(ID),
);

create table DC_INV_TYPE_MOVEMENT(
	ID varchar(15) PRIMARY KEY,
	Description VARCHAR(30),
	PositiveNegative BIT
);

create table DC_INV_INVENTORY_MOVEMENT(
	ID varchar(15) PRIMARY KEY,
	ID_Warehouse_Inventory varchar(15),
	Type varchar(15),
	Amount int,
	FOREIGN KEY (ID_Warehouse_Inventory) REFERENCES DC_INV_WAREHOUSE_INVENTORY(ID),
	FOREIGN KEY (Type) REFERENCES DC_INV_TYPE_MOVEMENT(ID)
);

create table DC_INV_SALE(
	ID varchar(15) PRIMARY KEY,
	ID_Warehouse varchar(15),
	Date datetime,
	Total decimal(15, 2),
	Discount int,
	Total_discount decimal(15, 2),
	State bit,
	Nit varchar(20),
	bill varchar(50),
	FOREIGN KEY (ID_Warehouse) REFERENCES DC_INV_WAREHOUSE(ID)
);

create table DC_INV_SALE_DETAIL(
	ID varchar(15) PRIMARY KEY,
	ID_Sale varchar(15),
	Line int,
	ID_Product varchar(15),
	Amount int,
	Price decimal(7,2),
	FOREIGN KEY (ID_Sale) REFERENCES DC_INV_SALE(ID),
	FOREIGN KEY (ID_Product) REFERENCES DC_INV_PRODUCT(ID)
);