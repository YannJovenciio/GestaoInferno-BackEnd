-- Script para criar categorias de teste
-- Execute este script no banco de dados SQLite (Hell.db)

-- Limpar categorias antigas (opcional)
-- DELETE FROM Categories;

-- Inserir categorias
INSERT INTO Categories (IdCategoria, NomeCategoria)
VALUES 
    ('550e8400-e29b-41d4-a716-446655440000', 'Demônios Infernais'),
    ('660e8400-e29b-41d4-a716-446655440001', 'Espíritos Maléficos'),
    ('770e8400-e29b-41d4-a716-446655440002', 'Criaturas Abissais');

-- Verificar inserção
SELECT * FROM Categories;
