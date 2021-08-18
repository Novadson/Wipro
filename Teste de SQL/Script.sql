--CREAT tb_Status TABLE
 CREATE TABLE tb_Status(
 	idStatus INT GENERATED ALWAYS AS IDENTITY,
 	dsStatus varchar(50),
 	PRIMARY KEY(idStatus)
 );

INSERT into
tb_Status (dsStatus) 
values 
('Finalizado'),('Pendente'),('Aguardando Aprovação');
 
select * from tb_status

 --CREAT tb_Processo TABLE
CREATE TABLE tb_Processo (
	idProcesso INT GENERATED ALWAYS AS identity,
	nroProcesso BIGINT,
	Autor varchar(90), 
    dtEntrada DATE NOT NULL,
    dtEncerramento DATE NOT NULL,
    idStatus INT NOT NULL,
    PRIMARY KEY(idProcesso),
   CONSTRAINT fk_tb_status
      FOREIGN KEY(idStatus) 
	  REFERENCES tb_status(idStatus)
	  ON DELETE CASCADE
);

select * from tb_Processo 

INSERT into
tb_Processo (nroProcesso,Autor,dtEntrada,dtEncerramento,idStatus) 
values 
(,'Jean Silva','01/02/2005','10/10/2013',1),
(20,'Silvana Pereira','20/07/2010','10/10/2013',1),
(30,'Rosi Belmonte','20/07/2010','10/10/2018',1),
(40,'Pedro Roberto','01/04/2011','10/10/2013',1),
(50,'Joseph Albert','01/04/2012','10/10/2013',1),
(60,'Juliana Paul','01/06/2004','10/10/2013',2),
(70,'Pedro Roberto','01/09/2011','10/10/2013',3),
(80,'Russo Piere','01/04/2010','10/04/2014',3);

select * from tb_status 
 --CREAT tb_andamento TABLE
CREATE TABLE tb_Andamento (
	idAndamento INT GENERATED ALWAYS AS identity,
	idProcesso INT,
    dtAndamento DATE NOT NULL,
    dsMovimento varchar(200),
    PRIMARY KEY(idAndamento),
   CONSTRAINT fk_tb_Processo
      FOREIGN KEY(idProcesso) 
	  REFERENCES tb_Processo(idProcesso)
	  ON DELETE CASCADE
);

INSERT into
tb_Andamento (idProcesso,dtAndamento,dsMovimento) 
values 
(1,'01/02/2010','Mov 1'),
(1,'10/04/2013','Mov 2'),
(3,'01/10/2009','Mov 3'),
(4,'01/03/2006','Mov 5');

--1. Com base no modelo acima, escreva um comando SQL que liste a quantidade de processos por 
--Status com sua descrição.
select count(*),ts.dsStatus from tb_processo tp 
inner join tb_status ts on tp.idStatus = ts.idStatus
where ts.dsStatus = 'Finalizado' group by ts.dsStatus


--2 Com base no modelo acima, construa um comando SQL que liste a maior data de andamento 
--por número de processo, com processos encerrados no ano de 2013.
select max(ta.dtAndamento) from tb_Andamento ta
inner join tb_processo tp on tp.idprocesso = ta.idprocesso 
where  date_part('year', tp.dtencerramento::date) = '2013'


-- 3 Com base no modelo acima, construa um comando SQL que liste a quantidade de Data de 
--Encerramento agrupada por ela mesma onde a quantidade da contagem seja maior que 5.
select count(tp.dtencerramento),tp.dtencerramento from tb_processo tp 
group by tp.dtencerramento
having count(tp.dtencerramento) >5


--Possuímos um número de identificação do processo, onde o mesmo contém 12 caracteres 
--com zero à esquerda, contudo nosso modelo e dados ele é apresentado como bigint. Como 
--fazer para apresenta-lo com 12 caracteres considerando os zeros a esquerda

select  LPAD(012345678901::text ,12, '0' ) AS nrIdentificacao





