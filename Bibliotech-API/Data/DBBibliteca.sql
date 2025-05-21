-- create database "Biblioteca";
-- use "Biblioteca";

CREATE TABLE "Corredor" (
    id_corredor SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL
);

CREATE TABLE "Estante" (
    id_estante SERIAL PRIMARY KEY,
    id_corredor_fk INT,
    nome VARCHAR(255) NOT NULL,
    FOREIGN KEY (id_corredor_fk) REFERENCES "Corredor"(id_corredor)
);

CREATE TABLE "Prateleira" (
    id_prateleira SERIAL PRIMARY KEY,
    id_estante_fk INT,
    posicao VARCHAR(255) NOT NULL,
    FOREIGN KEY (id_estante_fk) REFERENCES "Estante"(id_estante)
);

CREATE TABLE "Categoria" (
    id_categoria SERIAL PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL
);

CREATE TABLE "Autor" (
    id_autor SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL
);

CREATE TABLE "Livro" (
    id_livro SERIAL PRIMARY KEY,
    titulo VARCHAR(255) NOT NULL,
    id_autor_fk INT,
    FOREIGN KEY (id_autor_fk) REFERENCES "Autor"(id_autor)
);

CREATE TABLE "Livro_Categoria" (
    id_livro_categoria SERIAL PRIMARY KEY,
    id_categoria_fk INT,
    id_livro_fk INT,
    FOREIGN KEY (id_categoria_fk) REFERENCES "Categoria"(id_categoria),
    FOREIGN KEY (id_livro_fk) REFERENCES "Livro"(id_livro)
);

CREATE TABLE "Livro_Autor" (
    id_livro_autor SERIAL PRIMARY KEY,
    id_autor_fk INT,
    id_livro_fk INT,
    FOREIGN KEY (id_autor_fk) REFERENCES "Autor"(id_autor),
    FOREIGN KEY (id_livro_fk) REFERENCES "Livro"(id_livro)
);

CREATE TABLE "Cliente" (
    id_cliente SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    endereco VARCHAR(255),
    telefone VARCHAR(50),
    email VARCHAR(100)
);

CREATE TABLE "Funcionario" (
    id_funcionario SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    cargo VARCHAR(100),
    email VARCHAR(100),
    endereco VARCHAR(255),
    telefone VARCHAR(50)
);

CREATE TABLE "Emprestimo" (
    id_emprestimo SERIAL PRIMARY KEY,
    id_cliente_fk INT,
    id_funcionario_fk INT,
    id_livro_fk INT,
    data_emprestimo DATE,
    data_devolucao DATE,
    FOREIGN KEY (id_cliente_fk) REFERENCES "Cliente"(id_cliente),
    FOREIGN KEY (id_funcionario_fk) REFERENCES "Funcionario"(id_funcionario),
    FOREIGN KEY (id_livro_fk) REFERENCES "Livro"(id_livro)
);

CREATE TABLE "Reserva" (
    id_reserva SERIAL PRIMARY KEY,
    id_cliente_fk INT,
    id_livro_fk INT,
    data_reserva DATE,
    status VARCHAR(50),
    FOREIGN KEY (id_cliente_fk) REFERENCES "Cliente"(id_cliente),
    FOREIGN KEY (id_livro_fk) REFERENCES "Livro"(id_livro)
);