# Construction Control Journal

Управление строительными работами: журналы, контроль качества, инспекции, документооборот.

🔗 [Документация](docs/README.md)  
🎥 [Демо-скринкаст](assets/demo-screencast.mp4)  
📦 [Архитектура](docs/ARCHITECTURE.md)  
🚀 [Развертывание](docs/DEPLOYMENT.md)

## 🧱 Структура проекта

%% Use Case Diagram
graph TD
    subgraph Система управления строительными объектами
        %% Actors
        actorA[Служба строительного контроля]:::actor
        actorB[Прораб]:::actor
        actorC[Инспектор]:::actor
        
        %% Use Cases Grouped by Actor
        subgraph actorA_uses[Служба строительного контроля]
            uc1[Просмотр карточек объектов]:::usecase
            uc2[Активация объекта]:::usecase
            uc3[Назначение ответственного]:::usecase
            uc4[Согласование изменений графика]:::usecase
            uc5[Внесение замечаний]:::usecase
            uc6[Верификация выполненных работ]:::usecase
            uc7[Редактирование сетевого графика]:::usecase
        end
        
        subgraph actorB_uses[Прораб]
            uc8[Ввод данных о материалах]:::usecase
            uc9[Загрузка ТТН и паспортов]:::usecase
            uc10[Исправление замечаний]:::usecase
            uc11[Отметка выполненных работ]:::usecase
            uc12[Запрос изменений графика]:::usecase
        end
        
        subgraph actorC_uses[Инспектор]
            uc13[Согласование активации объекта]:::usecase
            uc14[Фиксация нарушений]:::usecase
            uc15[Проверка исправлений]:::usecase
            uc16[Инициирование лабораторных проб]:::usecase
        end
        
        %% Relationships
        actorA -->|Выполняет| uc1
        actorA -->|Выполняет| uc2
        actorA -->|Выполняет| uc3
        actorA -->|Выполняет| uc4
        actorA -->|Выполняет| uc5
        actorA -->|Выполняет| uc6
        actorA -->|Выполняет| uc7
        
        actorB -->|Выполняет| uc8
        actorB -->|Выполняет| uc9
        actorB -->|Выполняет| uc10
        actorB -->|Выполняет| uc11
        actorB -->|Выполняет| uc12
        
        actorC -->|Выполняет| uc13
        actorC -->|Выполняет| uc14
        actorC -->|Выполняет| uc15
        actorC -->|Выполняет| uc16
        

        
        %% Styles
        classDef actor fill:#f9f,stroke:#333,stroke-width:2px
        classDef usecase fill:#b9f,stroke:#333,stroke-width:1px
    end
