create database ProyectoFinalDB
create table Usuario(
nombreUser varchar(50) primary key not null,
pass varchar(50) not null,
esAdmin bit not null,
estaHabilitado bit not null
)
create table Cliente(
nombre varchar(50) not null,
cioRUT int primary key not null,
domicilio varchar(50) not null,
fachaNacimiento date not null,
estaHabilitado bit not null
)
create table Producto(
nombre varchar(50) not null,
marca varchar(50) not null,
idProducto int primary key not null,
precio int not null,
estaHabilitado bit not null
)
create table Factura(
idFactura int primary key not null,
fecha date not null,
cioRUT int not null,
foreign key (cioRUT) references Cliente(cioRUT),
montoTotal int not null,
estaHabilitada bit not null
)
create table Detalle(
IdDetalle int Identity(1,1) primary key not null,
idProducto int not null,
idFactura int not null,
cantidad int not null,
foreign key (idProducto) references Producto(idProducto),
foreign key (idFactura) references Factura(idFactura)
)

insert into Cliente values ('Adolfo', 49343158, 'Calle Falsa 123', '1997-07-18',1)
insert into Cliente values ('Nicolas', 11111111, 'Baker Street 221B', '1998-06-15',1)
insert into Cliente values ('Roberto', 22222222, '744 Evergreen Terrace', '1999-05-14',1)
insert into Cliente values ('Walter', 33333333, '308 Negra Arroyo Lane', '1996-08-13',1)
insert into Cliente values ('Noel', 44444444, 'Berwick Street', '1995-10-02',1)

insert into Producto values ('Jabón', 'Bulldog', 1234, 25,1)
insert into Producto values ('Detergente', 'Nevex', 4321, 40,1)
insert into Producto values ('Pure de papas', 'Puritas', 2314, 30,1)
insert into Producto values ('Aceite', 'Optimo', 2413, 25,1)

insert into Factura values (1,'2016-05-14', 49343158, 0,1)
insert into Factura values (2,'2017-10-18',11111111,0,1)
insert into Factura values (3,'2017-07-18',44444444,0,1)

insert into Detalle values (1234, 1, 2)
insert into Detalle values (2314, 3, 5)
insert into Detalle values (4321, 2, 3)
insert into Detalle values (2413, 1, 12)

insert into Usuario values ('fefffo', 'AdolfoCastelo1@', 1,1)
