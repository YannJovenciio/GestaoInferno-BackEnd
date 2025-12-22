-- ============================================================
-- Script para popular dados no banco Inferno
-- Executar no DBEaver ou SQLite Client
-- ============================================================

-- 1. CATEGORIAS
INSERT INTO Categories (IdCategoria, NomeCategoria)
VALUES 
    ('550e8400-e29b-41d4-a716-446655440000', 'Demônios Infernais'),
    ('660e8400-e29b-41d4-a716-446655440001', 'Espíritos Maléficos'),
    ('770e8400-e29b-41d4-a716-446655440002', 'Criaturas Abissais'),
    ('880e8400-e29b-41d4-a716-446655440003', 'Possuidores');

-- 2. DEMÔNIOS
INSERT INTO Demons (IdDemon, CategoryId, DemonName, CreatedAt, UpdatedAt)
VALUES 
    ('11110000-0000-0000-0000-000000000001', '550e8400-e29b-41d4-a716-446655440000', 'Belzebu', datetime('now'), NULL),
    ('22220000-0000-0000-0000-000000000002', '550e8400-e29b-41d4-a716-446655440000', 'Asmodeu', datetime('now'), NULL),
    ('33330000-0000-0000-0000-000000000003', '660e8400-e29b-41d4-a716-446655440001', 'Leviatã', datetime('now'), NULL),
    ('44440000-0000-0000-0000-000000000004', '660e8400-e29b-41d4-a716-446655440001', 'Azazel', datetime('now'), NULL),
    ('55550000-0000-0000-0000-000000000005', '770e8400-e29b-41d4-a716-446655440002', 'Baphomet', datetime('now'), NULL);

-- 3. CAVERNAS
INSERT INTO Caverns (IdCavern, Location, Capacity)
VALUES 
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Abismo Profundo', 50),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Caverna de Fogo', 100),
    ('cccccccc-cccc-cccc-cccc-cccccccccccc', 'Escuridão Eterna', 75);

-- 4. ALMAS (IMPORTANTE: Level agora é enum - 0=Inferior, 1=Medio, 2=Superior, 3=Maximo)
INSERT INTO Souls (IdSoul, Name, Description, Level, CavernId)
VALUES 
    ('eeee0000-0000-0000-0000-000000000001', 'Alma Perdida 1', 'Uma alma atormentada', 0, 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'),
    ('ffff0000-0000-0000-0000-000000000002', 'Alma Perdida 2', 'Condenada ao sofrimento', 2, 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'),
    ('gggg0000-0000-0000-0000-000000000003', 'Alma Perdida 3', 'Sem esperança', 3, 'cccccccc-cccc-cccc-cccc-cccccccccccc');

-- 5. PECADOS (IMPORTANTE: SinSeverity é enum - 0=Leve, 1=Moderado, 2=Severo, 3=Critico)
INSERT INTO Sins (IdSin, SinName, SinSeverity, IdSoul)
VALUES 
    ('hhhh0000-0000-0000-0000-000000000001', 'Gula', 0, 'eeee0000-0000-0000-0000-000000000001'),
    ('iiii0000-0000-0000-0000-000000000002', 'Ira', 2, 'ffff0000-0000-0000-0000-000000000002'),
    ('jjjj0000-0000-0000-0000-000000000003', 'Preguiça', 1, 'gggg0000-0000-0000-0000-000000000003');

-- 6. HELL (Opcional - pode ser usado para registrar os níveis do inferno)
INSERT INTO Hell (IdHell, Nome, Descricao, Nivel)
VALUES 
    ('zzzz0000-0000-0000-0000-000000000001', 'Primeiro Círculo', 'Luxúria', 0),
    ('zzzz0000-0000-0000-0000-000000000002', 'Segundo Círculo', 'Gula', 1),
    ('zzzz0000-0000-0000-0000-000000000003', 'Terceiro Círculo', 'Ganância', 2);

-- 7. PERSEGUIÇÕES (N:N Demônio-Alma) - Tabela de junção
INSERT INTO Persecutions (IdDemon, IdSoul, DataInicio, DataFim)
VALUES 
    ('11110000-0000-0000-0000-000000000001', 'eeee0000-0000-0000-0000-000000000001', datetime('now'), NULL),
    ('22220000-0000-0000-0000-000000000002', 'ffff0000-0000-0000-0000-000000000002', datetime('2024-01-01'), datetime('2024-12-31')),
    ('33330000-0000-0000-0000-000000000003', 'gggg0000-0000-0000-0000-000000000003', datetime('now'), NULL);

-- 8. REALIZAÇÕES (N:N Pecado-Alma) - Tabela de junção
INSERT INTO Realizes (IdSin, IdSoul)
VALUES 
    ('hhhh0000-0000-0000-0000-000000000001', 'eeee0000-0000-0000-0000-000000000001'),
    ('iiii0000-0000-0000-0000-000000000002', 'ffff0000-0000-0000-0000-000000000002'),
    ('jjjj0000-0000-0000-0000-000000000003', 'gggg0000-0000-0000-0000-000000000003');

-- ============================================================
-- VERIFICAÇÕES (descomente para ver os dados)
-- ============================================================

-- SELECT * FROM Categories;
-- SELECT * FROM Demons;
-- SELECT * FROM Caverns;
-- SELECT * FROM Souls;
-- SELECT * FROM Sins;
-- SELECT * FROM Hell;
-- SELECT * FROM Persecutions;
-- SELECT * FROM Realizes;

-- ============================================================
-- QUERY COM JOIN (para ver relacionamentos)
-- ============================================================

-- Demônios com suas categorias
-- SELECT d.DemonName, c.NomeCategoria 
-- FROM Demons d 
-- LEFT JOIN Categories c ON d.CategoryId = c.IdCategoria;

-- Almas em cavernas
-- SELECT s.Name, c.Location, c.Capacity 
-- FROM Souls s 
-- LEFT JOIN Caverns c ON s.CavernId = c.IdCavern;

-- Perseguições ativas
-- SELECT d.DemonName, s.Name, p.DataInicio, p.DataFim 
-- FROM Persecutions p
-- JOIN Demons d ON p.IdDemon = d.IdDemon
-- JOIN Souls s ON p.IdSoul = s.IdSoul
-- WHERE p.DataFim IS NULL;

-- Pecados de cada alma
-- SELECT s.Name, sin.SinName, sin.SinSeverity 
-- FROM Souls s
-- JOIN Sins sin ON s.IdSoul = sin.IdSoul;
