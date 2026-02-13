Event Platform — MVP Event Driven Architecture

Proyecto MVP de una plataforma de eventos online diseñada con arquitectura basada en microservicios, mensajería asíncrona y frontend React mínimo para validación técnica.

Este repositorio demuestra:

✅ Diseño orientado a eventos
✅ Comunicación asincrónica con RabbitMQ
✅ Persistencia SQL con EF Core
✅ Cache Redis
✅ Consumidor idempotente
✅ Docker multi-servicio
✅ Frontend React integrado
✅ Arquitectura lista para escalar

Arquitectura

El sistema sigue un enfoque event-driven microservices:

Frontend React
→ EventService API (.NET)
→ SQL Server + Redis
→ RabbitMQ (broker)
→ NotificationService (.NET Worker)

React → EventService → SQL
       → Redis Cache
       → RabbitMQ → NotificationService
Servicios
Servicio	Responsabilidad
EventService	Registro de eventos y publicación de mensajes
NotificationService	Consumo idempotente de eventos
SQL Server	Persistencia transaccional
Redis	Cache de lectura
RabbitMQ	Mensajería asincrónica
Stack tecnológico

Backend:

.NET 8

EF Core

MassTransit

SQL Server

Redis

RabbitMQ

Frontend:

React + TypeScript

Vite

Infraestructura:

Docker

Docker Compose

Cómo ejecutar el proyecto
1. Requisitos

Docker Desktop
Node.js 18+
.NET SDK 8+
2. Levantar infraestructura

Desde la raíz del repo:

docker compose up --build

Esto levanta:

SQL Server

Redis

RabbitMQ

EventService

NotificationService

3. Frontend
cd frontend/event-ui
npm install
npm run dev

Abrir:

http://localhost:5173

Endpoints
Crear evento
POST /events

Body:

{
  "name": "Rock Festival",
  "date": "2026-02-14T20:00:00",
  "place": "Arena Lima",
  "zones": [
    { "name": "VIP", "price": 150, "capacity": 100 },
    { "name": "General", "price": 50, "capacity": 500 }
  ]
}
Listar eventos
GET /events

Cacheado en Redis por 1 minuto.

 Flujo de eventos

Admin crea evento

EventService guarda en SQL (transacción)

Publica EventCreated a RabbitMQ

NotificationService consume mensaje

Valida idempotencia

Registra mensaje procesado

Formato de evento:

{
  "messageId": "uuid",
  "eventId": "uuid",
  "name": "string",
  "occurredAt": "ISO-8601",
  "correlationId": "uuid",
  "version": 1
}
🛡 Idempotencia

NotificationService guarda:

ProcessedMessages(messageId)

Si el mensaje ya fue procesado:

→ se ignora

Esto evita duplicaciones.

⚙️ Decisiones técnicas
Arquitectura event-driven

Permite desacoplar servicios y escalar independientemente.

Redis cache

Optimiza lecturas de eventos frecuentes.

SQL transaccional

Garantiza consistencia de datos.

RabbitMQ

Mensajería confiable con soporte para reintentos.

Docker

Entorno reproducible y portable.

📈 Escalabilidad futura

El sistema está preparado para:

Pagos PSP

Marketplace

BI / analytics

Promociones

Tickets QR

Autenticación OIDC

Microservicios adicionales

Validación MVP

Este proyecto cumple los objetivos del reto:

✔ Persistencia
✔ Arquitectura limpia
✔ Event-driven
✔ Frontend React
✔ Docker
✔ Idempotencia
✔ Cache
✔ Mensajería

Autor

Reynaldo Yovera
Senior Software Engineer
Event-driven architecture enthusiast
