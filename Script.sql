create table Emissora(
	Id int not null identity(1,1) primary key,
	Nome varchar(50) not null
)

create table Audiencia(
	Id int not null identity(1,1) primary key,
	Pontos_Audiencia float not null,
	Data_Hora_Audiencia smalldatetime not null,
	Emissora_Audiencia int not null,
	constraint PK_Audiencia_Emissora foreign key 
	(Emissora_Audiencia) references Emissora(Id)
)