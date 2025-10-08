# 🚀 INSTRUCCIONES COMPLETAS DE IMPLEMENTACIÓN - DondeComemos

## 📋 LISTA DE VERIFICACIÓN ANTES DE EMPEZAR

- [ ] Visual Studio Code o Visual Studio instalado
- [ ] .NET 9.0 SDK instalado
- [ ] SQLite instalado
- [ ] Git instalado (opcional)

---

## 🔧 PASO 1: ACTUALIZAR MODELOS

### 1.1 Actualizar UserProfile.cs

**Ubicación:** `Models/UserProfile.cs`

**Acción:** REEMPLAZAR completamente el archivo con el código del artifact `profile_model`

---

## 🎮 PASO 2: ACTUALIZAR CONTROLADORES EXISTENTES

### 2.1 Actualizar RestaurantesController.cs

**Ubicación:** `Controllers/RestaurantesController.cs`

**Acción:** REEMPLAZAR completamente con el artifact `restaurantes_controller_fixed`

### 2.2 Actualizar ProfileController.cs

**Ubicación:** `Controllers/ProfileController.cs`

**Acción:** REEMPLAZAR completamente con el artifact `profile_controller_improved`

---

## ➕ PASO 3: CREAR NUEVOS CONTROLADORES

### 3.1 Crear ProductosController.cs

**Ubicación:** `Controllers/ProductosController.cs` (NUEVO ARCHIVO)

**Acción:** CREAR archivo nuevo con el artifact `productos_controller`

### 3.2 Crear ApiController.cs

**Ubicación:** `Controllers/ApiController.cs` (NUEVO ARCHIVO)

**Acción:** CREAR archivo nuevo con el artifact `api_controller`

---

## 🖼️ PASO 4: ACTUALIZAR VISTAS DE HOME

### 4.1 Actualizar _Hero.cshtml

**Ubicación:** `Views/Home/Partials/_Hero.cshtml`

**Acción:** REEMPLAZAR con el artifact `hero_fixed`

### 4.2 Actualizar _Mapa.cshtml

**Ubicación:** `Views/Home/Partials/_Mapa.cshtml`

**Acción:** REEMPLAZAR con el artifact `mapa_fixed`

### 4.3 Actualizar _CTA.cshtml

**Ubicación:** `Views/Home/Partials/_CTA.cshtml`

**Acción:** REEMPLAZAR con el artifact `cta_fixed`

### 4.4 Actualizar Index.cshtml de Home

**Ubicación:** `Views/Home/Index.cshtml`

**Acción:** REEMPLAZAR con el artifact `home_index_improved`

---

## 🍽️ PASO 5: ACTUALIZAR VISTAS DE RESTAURANTES

### 5.1 Actualizar Create.cshtml

**Ubicación:** `Views/Restaurantes/Create.cshtml`

**Acción:** REEMPLAZAR con el artifact `restaurante_create_maps`

### 5.2 Actualizar Edit.cshtml

**Ubicación:** `Views/Restaurantes/Edit.cshtml`

**Acción:** REEMPLAZAR con el artifact `restaurante_edit_maps`

### 5.3 Actualizar Details.cshtml

**Ubicación:** `Views/Restaurantes/Details.cshtml`

**Acción:** REEMPLAZAR con el artifact `restaurante_details`

### 5.4 Actualizar Delete.cshtml

**Ubicación:** `Views/Restaurantes/Delete.cshtml`

**Acción:** REEMPLAZAR con el artifact `restaurante_delete_fixed`

---

## 📦 PASO 6: CREAR VISTAS DE PRODUCTOS

### 6.1 Crear carpeta Productos

**Acción:** Crear la carpeta `Views/Productos/`

### 6.2 Crear Create.cshtml

**Ubicación:** `Views/Productos/Create.cshtml` (NUEVO ARCHIVO)

**Acción:** CREAR con el artifact `producto_create`

### 6.3 Crear Edit.cshtml

**Ubicación:** `Views/Productos/Edit.cshtml` (NUEVO ARCHIVO)

**Acción:** CREAR con el artifact `producto_edit`

### 6.4 Crear Delete.cshtml

**Ubicación:** `Views/Productos/Delete.cshtml` (NUEVO ARCHIVO)

**Acción:** CREAR con el artifact `producto_delete`

### 6.5 Crear Index.cshtml

**Ubicación:** `Views/Productos/Index.cshtml` (NUEVO ARCHIVO)

**Acción:** CREAR con el artifact `producto_index`

---

## 👤 PASO 7: ACTUALIZAR VISTA DE PERFIL

### 7.1 Actualizar Profile/Index.cshtml

**Ubicación:** `Views/Profile/Index.cshtml`

**Acción:** REEMPLAZAR completamente con el artifact `profile_index_complete`

---

## ⚙️ PASO 8: ACTUALIZAR CONFIGURACIÓN

### 8.1 Actualizar appsettings.json

**Ubicación:** `appsettings.json`

**Acción:** REEMPLAZAR con el artifact `appsettings_updated`

---

## 🗄️ PASO 9: EJECUTAR MIGRACIONES

Abre una terminal en la carpeta del proyecto y ejecuta:

```bash
# Crear nueva migración
dotnet ef migrations add ActualizarModelos

# Aplicar migración a la base de datos
dotnet ef database update
```

**Si tienes errores con dotnet ef:**

```bash
# Instalar herramienta EF Core
dotnet tool install --global dotnet-ef

# Intentar nuevamente
dotnet ef migrations add ActualizarModelos
dotnet ef database update
```

---

## 📁 PASO 10: CREAR CARPETAS PARA IMÁGENES

En la terminal, dentro de la carpeta del proyecto:

**Windows (PowerShell):**
```powershell
New-Item -Path "wwwroot/uploads" -ItemType Directory -Force
New-Item -Path "wwwroot/uploads/restaurantes" -ItemType Directory -Force
New-Item -Path "wwwroot/uploads/productos" -ItemType Directory -Force
New-Item -Path "wwwroot/uploads/perfiles" -ItemType Directory -Force
```

**Linux/Mac:**
```bash
mkdir -p wwwroot/uploads/restaurantes
mkdir -p wwwroot/uploads/productos
mkdir -p wwwroot/uploads/perfiles
```

---

## 🗺️ PASO 11: OBTENER API KEY DE GOOGLE MAPS

### 11.1 Ir a Google Cloud Console

1. Ve a: https://console.cloud.google.com/
2. Inicia sesión con tu cuenta de Google

### 11.2 Crear Proyecto

1. Haz clic en "Seleccionar proyecto" (arriba)
2. Clic en "NUEVO PROYECTO"
3. Nombre: "DondeComemos"
4. Clic en "CREAR"

### 11.3 Habilitar APIs

1. En el menú lateral, ve a "APIs y servicios" > "Biblioteca"
2. Busca y habilita estas APIs:
   - **Maps JavaScript API**
   - **Geocoding API**
   - **Places API** (opcional)

### 11.4 Crear Credenciales

1. Ve a "APIs y servicios" > "Credenciales"
2. Clic en "+ CREAR CREDENCIALES"
3. Selecciona "Clave de API"
4. Copia la API Key generada

### 11.5 Restringir API Key (IMPORTANTE)

1. Haz clic en tu API Key
2. En "Restricciones de aplicación", selecciona "Referentes HTTP"
3. Agrega: `http://localhost:*` y `https://localhost:*`
4. En "Restricciones de API", selecciona las APIs habilitadas
5. Guarda

### 11.6 Configurar en el proyecto

**Opción 1: En appsettings.json** (NO recomendado para producción)

```json
{
  "GoogleMaps": {
    "ApiKey": "TU_API_KEY_AQUI"
  }
}
```

**Opción 2: Variables de entorno** (RECOMENDADO)

```bash
# Windows
$env:GoogleMaps__ApiKey="TU_API_KEY_AQUI"

# Linux/Mac
export GoogleMaps__ApiKey="TU_API_KEY_AQUI"
```

### 11.7 Actualizar las vistas

En TODOS los archivos que contengan:
```html
<script src="https://maps.googleapis.com/maps/api/js?key=TU_API_KEY_AQUI
```

Reemplaza `TU_API_KEY_AQUI` con tu API Key real.

**Archivos a actualizar:**
- `Views/Home/Index.cshtml`
- `Views/Restaurantes/Create.cshtml`
- `Views/Restaurantes/Edit.cshtml`
- `Views/Restaurantes/Details.cshtml`

---

## 🔒 PASO 12: ACTUALIZAR .gitignore

Agrega al final de tu archivo `.gitignore`:

```
# Imágenes subidas
/wwwroot/uploads/*

# API Keys
appsettings.Production.json
```

---

## ▶️ PASO 13: EJECUTAR LA APLICACIÓN

```bash
dotnet run
```

O presiona **F5** en Visual Studio / VS Code

---

## ✅ PASO 14: VERIFICAR FUNCIONALIDADES

### 14.1 Verificar Home
- [ ] La página principal carga correctamente
- [ ] El botón "Iniciar la búsqueda" funciona
- [ ] El mapa se carga en la página principal
- [ ] Los marcadores aparecen en el mapa

### 14.2 Verificar Autenticación
- [ ] El botón de "Iniciar Sesión" se muestra cuando NO estás autenticado
- [ ] El botón de "Iniciar Sesión" desaparece cuando estás autenticado
- [ ] Aparece el menú de usuario cuando estás autenticado

### 14.3 Verificar Restaurantes (Admin)
- [ ] Crear nuevo restaurante con ubicación en mapa
- [ ] Editar restaurante existente
- [ ] Eliminar restaurante (debe funcionar sin errores)
- [ ] Ver detalles de restaurante público

### 14.4 Verificar Productos (Admin)
- [ ] Agregar productos al menú de un restaurante
- [ ] Editar productos
- [ ] Eliminar productos
- [ ] Ver menú en vista pública

### 14.5 Verificar Perfil de Usuario
- [ ] Editar información personal
- [ ] Cambiar foto de perfil
- [ ] Cambiar contraseña
- [ ] Ver historial de búsquedas
- [ ] Agregar/quitar favoritos

### 14.6 Verificar Búsqueda
- [ ] Buscar restaurantes por nombre
- [ ] Filtrar por tipo de cocina
- [ ] Filtrar por calificación
- [ ] Buscar restaurantes cercanos con GPS

---

## 🐛 SOLUCIÓN DE PROBLEMAS COMUNES

### Error: "No se puede encontrar la tabla Restaurantes"
**Solución:**
```bash
dotnet ef database update
```

### Error: "API Key inválida" en Google Maps
**Solución:**
1. Verifica que las APIs estén habilitadas
2. Verifica las restricciones de la API Key
3. Espera 5 minutos (puede tardar en activarse)

### Error al subir imágenes
**Solución:**
1. Verifica que las carpetas en `wwwroot/uploads/` existan
2. Verifica permisos de escritura en la carpeta

### Error: "No se puede eliminar restaurante"
**Solución:**
- Asegúrate de haber actualizado el `RestaurantesController.cs` con el código corregido

### La búsqueda no funciona
**Solución:**
- Verifica que hayas actualizado `_Hero.cshtml` y `_Mapa.cshtml` con `asp-controller="Restaurantes"`

---

## 📊 RESUMEN DE CAMBIOS

### ✅ Funcionalidades Implementadas:

1. **Sistema de Autenticación mejorado**
   - Botones dinámicos según estado de autenticación
   - Perfil de usuario completo

2. **Gestión de Restaurantes**
   - CRUD completo con Google Maps
   - Ubicación GPS precisa
   - Eliminación corregida

3. **Gestión de Productos/Menú**
   - CRUD completo de productos
   - Categorización
   - Gestión de disponibilidad

4. **Perfil de Usuario**
   - Datos personales completos
   - Historial de búsquedas
   - Restaurantes favoritos
   - Cambio de contraseña

5. **Búsqueda Avanzada**
   - Filtros múltiples
   - Búsqueda por ubicación GPS
   - Restaurantes cercanos

6. **Google Maps**
   - Integración en home
   - Selección de ubicación en formularios
   - Vista de ubicación en detalles

---

## 📞 SOPORTE

Si encuentras algún error durante la implementación:

1. Verifica que hayas seguido todos los pasos en orden
2. Revisa los mensajes de error en la consola
3. Verifica que todas las migraciones se hayan aplicado
4. Comparte el mensaje de error específico para ayuda adicional

---

## 🎉 ¡LISTO!

Tu aplicación DondeComemos ahora tiene todas las funcionalidades implementadas.

**Próximos pasos opcionales:**
- Agregar sistema de reseñas
- Implementar reservaciones
- Agregar más filtros de búsqueda
- Implementar notificaciones
- Agregar panel de estadísticas para admin

¡Éxito con tu proyecto! 🚀