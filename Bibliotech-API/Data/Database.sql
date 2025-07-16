DROP DATABASE IF EXISTS biblioteca;
CREATE DATABASE biblioteca;
USE biblioteca;

CREATE TABLE corredores
(
    id     INT AUTO_INCREMENT PRIMARY KEY,
    numero INT NOT NULL
);

CREATE TABLE estantes
(
    id          INT AUTO_INCREMENT PRIMARY KEY,
    id_corredor INT NOT NULL,
    numero      INT NOT NULL,
    FOREIGN KEY (id_corredor) REFERENCES corredores (id)
);

CREATE TABLE prateleiras
(
    id         INT AUTO_INCREMENT PRIMARY KEY,
    id_estante INT NOT NULL,
    numero     INT NOT NULL,
    FOREIGN KEY (id_estante) REFERENCES estantes (id)
);

CREATE TABLE categorias
(
    id   INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(255) NOT NULL
);

CREATE TABLE autores
(
    id   INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(255) NOT NULL
);

CREATE TABLE livros
(
    id            INT AUTO_INCREMENT PRIMARY KEY,
    id_prateleira INT          NOT NULL,
    titulo        VARCHAR(255) NOT NULL,
    FOREIGN KEY (id_prateleira) REFERENCES prateleiras (id)
);

CREATE TABLE exemplares
(
    id       INT AUTO_INCREMENT PRIMARY KEY,
    id_livro INT                                                                       NOT NULL,
    situacao ENUM ('Normal', 'Danificado', 'EmManutencao', 'Extraviado', 'Descartado') NOT NULL DEFAULT 'Normal',
    FOREIGN KEY (id_livro) REFERENCES livros (id)
);

CREATE TABLE livro_categoria
(
    id_categoria INT NOT NULL,
    id_livro     INT NOT NULL,
    FOREIGN KEY (id_categoria) REFERENCES categorias (id),
    FOREIGN KEY (id_livro) REFERENCES livros (id),
    UNIQUE (id_categoria, id_livro)
);

CREATE TABLE livro_autor
(
    id_autor INT NOT NULL,
    id_livro INT NOT NULL,
    FOREIGN KEY (id_autor) REFERENCES autores (id),
    FOREIGN KEY (id_livro) REFERENCES livros (id),
    UNIQUE (id_autor, id_livro)
);

CREATE TABLE usuarios
(
    id       INT AUTO_INCREMENT PRIMARY KEY,
    cpf      VARCHAR(11)  NOT NULL UNIQUE,
    nome     VARCHAR(100) NOT NULL,
    email    VARCHAR(100) NOT NULL UNIQUE,
    senha    VARCHAR(255) NOT NULL,
    endereco VARCHAR(255),
    telefone VARCHAR(50)  NOT NULL
);

CREATE TABLE perfis
(
    id        INT AUTO_INCREMENT PRIMARY KEY,
    nome      VARCHAR(100) NOT NULL,
    descricao VARCHAR(255)
);

CREATE TABLE usuario_perfil
(
    id_usuario INT NOT NULL,
    id_perfil  INT NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id),
    FOREIGN KEY (id_perfil) REFERENCES perfis (id),
    UNIQUE (id_usuario, id_perfil)
);

CREATE TABLE emprestimos
(
    id                     INT AUTO_INCREMENT PRIMARY KEY,
    id_exemplar            INT                                                                      NOT NULL,
    id_usuario_leitor      INT                                                                      NOT NULL,
    id_usuario_responsavel INT                                                                      NOT NULL,
    status                 ENUM ('EmAndamento', 'Concluido', 'Atrasado', 'Extraviado', 'Cancelado') NOT NULL,
    data_inicio            DATE                                                                     NOT NULL,
    data_fim               DATE                                                                     NOT NULL,
    data_devolucao         DATE,
    danificado             BOOLEAN,
    observacao             TEXT,
    FOREIGN KEY (id_usuario_leitor) REFERENCES usuarios (id),
    FOREIGN KEY (id_usuario_responsavel) REFERENCES usuarios (id),
    FOREIGN KEY (id_exemplar) REFERENCES exemplares (id)
);

CREATE TABLE reservas
(
    id          INT AUTO_INCREMENT PRIMARY KEY,
    id_exemplar INT                                                                           NOT NULL,
    id_usuario  INT                                                                           NOT NULL,
    status      ENUM ('Pendente', 'AguardandoRetirada', 'Concluida', 'Cancelada', 'Expirada') NOT NULL,
    data_inicio DATE                                                                          NOT NULL,
    data_fim    DATE                                                                          NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuarios (id),
    FOREIGN KEY (id_exemplar) REFERENCES exemplares (id)
);


# Data exemple -------------------
INSERT INTO corredores (numero)
VALUES (1),
       (2),
       (3),
       (4);

INSERT INTO estantes (id_corredor, numero)
VALUES (1, 101),
       (1, 102),
       (2, 201),
       (2, 202),
       (3, 301),
       (4, 401);

INSERT INTO prateleiras (id_estante, numero)
VALUES (1, 1),
       (1, 2),
       (1, 3),
       (2, 1),
       (2, 2),
       (3, 1),
       (3, 2),
       (4, 1),
       (5, 1),
       (6, 1);

INSERT INTO categorias (nome)
VALUES ('Ficção Científica'),
       ('Fantasia'),
       ('Romance'),
       ('História'),
       ('Programação'),
       ('Suspense'),
       ('Aventura'),
       ('Biografia'),
       ('Saúde e Bem-Estar'),
       ('Infantil');

INSERT INTO autores (nome)
VALUES ('Isaac Asimov'),
       ('J.R.R. Tolkien'),
       ('Jane Austen'),
       ('Machado de Assis'),
       ('Martin Fowler'),
       ('George Orwell'),
       ('Agatha Christie'),
       ('Stephen King'),
       ('Yuval Noah Harari'),
       ('Mauricio de Sousa');

INSERT INTO livros (id_prateleira, titulo)
VALUES (1, 'A Fundação'),
       (1, 'Eu, Robô'),
       (2, 'O Fim da Eternidade'),
       (3, 'O Senhor dos Anéis'),
       (3, 'O Hobbit'),
       (4, 'Orgulho e Preconceito'),
       (4, 'Razão e Sensibilidade'),
       (5, 'Dom Casmurro'),
       (5, 'Memórias Póstumas de Brás Cubas'),
       (6, 'Refatoração: Aperfeiçoando o Design de Código Existente'),
       (7, '1984'),
       (8, 'Assassinato no Expresso Oriente'),
       (9, 'O Iluminado'),
       (10, 'Sapiens: Uma Breve História da Humanidade'),
       (10, 'A Turma da Mônica - Férias');

INSERT INTO exemplares (id_livro, situacao)
VALUES (1, 'Normal'),
       (1, 'Normal'),
       (1, 'Danificado'),
       (2, 'Normal'),
       (2, 'Normal'),
       (3, 'Normal'),
       (4, 'Normal'),
       (4, 'Normal'),
       (5, 'Normal'),
       (5, 'EmManutencao'),
       (6, 'Normal'),
       (7, 'Normal'),
       (8, 'Normal'),
       (9, 'Normal'),
       (10, 'Normal'),
       (11, 'Normal'),
       (12, 'Normal'),
       (13, 'Normal'),
       (14, 'Normal'),
       (15, 'Normal');

INSERT INTO livro_categoria (id_livro, id_categoria)
VALUES (1, 1),
       (2, 1),
       (3, 1),
       (4, 2),
       (5, 2),
       (6, 3),
       (7, 3),
       (8, 6),
       (9, 6),
       (10, 5),
       (11, 4),
       (12, 6),
       (13, 6),
       (14, 4),
       (15, 10);

INSERT INTO livro_autor (id_livro, id_autor)
VALUES (1, 1),
       (2, 1),
       (3, 1),
       (4, 2),
       (5, 2),
       (6, 3),
       (7, 3),
       (8, 4),
       (9, 4),
       (10, 5),
       (11, 6),
       (12, 7),
       (13, 8),
       (14, 9),
       (15, 10);

INSERT INTO usuarios (cpf, nome, email, senha, endereco, telefone)
VALUES ('11122233344', 'Joana Admin', 'joana.admin@biblioteca.com', 'senhaAdmin123', 'Rua Central, 100', '11987654321'),
       ('55566677788', 'Pedro Leitor', 'pedro.leitor@email.com', 'senhaLeitor', 'Av. das Arvores, 50', '21998765432'),
       ('99988877766', 'Ana Bibliotecaria', 'ana.biblio@biblioteca.com', 'senhaBiblio', 'Travessa da Paz, 789',
        '31987651234');

INSERT INTO perfis (nome, descricao)
VALUES ('Admin', 'Acesso completo ao sistema de gerenciamento.'),
       ('Bibliotecario', 'Gerenciamento de acervo, empréstimos e usuários.'),
       ('Leitor', 'Acesso a consulta de livros, empréstimos e reservas.');

INSERT INTO usuario_perfil (id_usuario, id_perfil)
VALUES (1, 1),
       (1, 2),
       (2, 3),
       (3, 2);

INSERT INTO emprestimos (id_exemplar, id_usuario_leitor, id_usuario_responsavel, status, data_inicio, data_fim,
                         data_devolucao, danificado, observacao)
VALUES (1, 2, 1, 'EmAndamento', '2025-07-10', '2025-07-24', NULL, FALSE, NULL),
       (4, 2, 1, 'Atrasado', '2025-06-01', '2025-06-15', NULL, FALSE, 'Livro 1984 com atraso de mais de um mês.'),
       (6, 2, 3, 'Concluido', '2025-05-01', '2025-05-15', '2025-05-14', FALSE, NULL),
       (10, 2, 3, 'Extraviado', '2024-01-01', '2024-01-15', NULL, TRUE,
        'Exemplar perdido pelo leitor. Passou de 60 dias do atraso.');

INSERT INTO reservas (id_exemplar, id_usuario, status, data_inicio, data_fim)
VALUES (2, 2, 'Pendente', '2025-07-15', '2025-07-22'),
       (5, 2, 'AguardandoRetirada', '2025-07-16', '2025-07-20');