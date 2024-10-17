-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-10-2024 a las 04:08:50
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `mercy_developer`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente`
--

CREATE TABLE `cliente` (
  `idCliente` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Apellido` varchar(45) NOT NULL,
  `Correo` varchar(45) NOT NULL,
  `Telefono` varchar(45) DEFAULT NULL,
  `Direccion` varchar(45) DEFAULT NULL,
  `Estado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `cliente`
--

INSERT INTO `cliente` (`idCliente`, `Nombre`, `Apellido`, `Correo`, `Telefono`, `Direccion`, `Estado`) VALUES
(1, 'bastian', 'guardas', 'bastian.guarda.morales@cftmail.cl', '9188787890', 'Acacias 999', 1),
(2, 'Lucho', 'Beliga', 'lucho@gmail.com', '234242424', 'Bolonegsi', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `datosfichatecnica`
--

CREATE TABLE `datosfichatecnica` (
  `idDatosFichaTecnica` int(11) NOT NULL,
  `FechaInicio` datetime DEFAULT NULL,
  `FechaFinalizacion` datetime DEFAULT NULL,
  `PObservacionesRecomendaciones` varchar(2000) DEFAULT NULL COMMENT 'Por el Tecnico',
  `SOInstalado` int(11) DEFAULT NULL COMMENT '0:Windows 10 ; 1: Windows 11; 2: Linux',
  `SuiteOfficeInstalada` int(11) DEFAULT NULL COMMENT '0: Microsoft Office 2013 ; 1: Microsoft Office 2019 ; 2: Microsoft Office 365 ; 3: OpenOffice',
  `LectorPDFInstalado` int(11) DEFAULT NULL COMMENT '0:No Instalado ; 1: Instalado 2: No aplica',
  `NavegadorWebInstalado` int(11) DEFAULT NULL COMMENT '0:No instalado ; 1: Chrome ; 2: Firefox; 3: Chrome y Firefox',
  `Antivirus Instalado` varchar(100) DEFAULT NULL,
  `RecepcionEquipoId` int(11) NOT NULL,
  `Estado` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `datosfichatecnica`
--

INSERT INTO `datosfichatecnica` (`idDatosFichaTecnica`, `FechaInicio`, `FechaFinalizacion`, `PObservacionesRecomendaciones`, `SOInstalado`, `SuiteOfficeInstalada`, `LectorPDFInstalado`, `NavegadorWebInstalado`, `Antivirus Instalado`, `RecepcionEquipoId`, `Estado`) VALUES
(5, '2024-10-15 22:42:00', '2024-10-15 22:42:00', '1', 1, 1, 1, 1, '1', 7, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `descripcionservicio`
--

CREATE TABLE `descripcionservicio` (
  `idDescServ` int(11) NOT NULL,
  `Nombre` text NOT NULL,
  `Servicio_idServicio` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `descripcionservicio`
--

INSERT INTO `descripcionservicio` (`idDescServ`, `Nombre`, `Servicio_idServicio`) VALUES
(10, 'descripcion de test', 8);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `diagnosticosolucion`
--

CREATE TABLE `diagnosticosolucion` (
  `idDiagnosticoSolucion` int(11) NOT NULL,
  `DescripcionDiagnostico` varchar(1000) NOT NULL,
  `DescripcionSolucion` varchar(1000) DEFAULT NULL,
  `DatosFichaTecnicaId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recepcionequipo`
--

CREATE TABLE `recepcionequipo` (
  `id` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `IdServicio` int(11) NOT NULL,
  `Fecha` datetime DEFAULT NULL,
  `TipoPc` int(11) DEFAULT NULL COMMENT '0: PC torre; 1: Notebook ; 2: all-in-one; 3: Desconocido;',
  `Accesorio` varchar(45) DEFAULT NULL,
  `MarcaPc` varchar(45) DEFAULT NULL,
  `ModeloPc` varchar(45) DEFAULT NULL,
  `NSerie` varchar(45) DEFAULT NULL,
  `CapacidadRam` int(11) DEFAULT NULL COMMENT '0: 4Gb; 1: 6Gb; 2: 8Gb; 3: 16Gb; 4:32Gb ;5: 64Gb; 6: 128Gb;',
  `TipoAlmacenamiento` int(11) DEFAULT NULL COMMENT '0: HDD; 1: SSD ; 2: SSD M.2 ; SSD NVM M.2;',
  `CapacidadAlmacenamiento` varchar(45) DEFAULT NULL,
  `TipoGpu` int(11) DEFAULT NULL COMMENT '0: Chip Integrado; 1: Tarjeta dedicada; 2: Otro;',
  `Grafico` varchar(45) DEFAULT NULL,
  `Estado` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `recepcionequipo`
--

INSERT INTO `recepcionequipo` (`id`, `IdCliente`, `IdServicio`, `Fecha`, `TipoPc`, `Accesorio`, `MarcaPc`, `ModeloPc`, `NSerie`, `CapacidadRam`, `TipoAlmacenamiento`, `CapacidadAlmacenamiento`, `TipoGpu`, `Grafico`, `Estado`) VALUES
(7, 1, 8, '2024-10-03 22:42:00', 1, '1', '1', '1', '1', 1, 1, '1', 1, '1', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `servicio`
--

CREATE TABLE `servicio` (
  `idServicio` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Precio` int(11) NOT NULL,
  `Sku` varchar(45) DEFAULT NULL,
  `Usuario_idUsuario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `servicio`
--

INSERT INTO `servicio` (`idServicio`, `Nombre`, `Precio`, `Sku`, `Usuario_idUsuario`) VALUES
(8, 'servicio test', 99999, 'FO001', 12);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Apellido` varchar(45) NOT NULL,
  `Correo` varchar(45) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `Nombre`, `Apellido`, `Correo`, `password`) VALUES
(5, 'Bastia', 'Guardañ', 'bastian.guarda.morales@cftmail.cl', '$2a$11$BfEc1eG84YIPq7EwDdmSKOIXitwqCH0hgzoODnvGDjezvaWMtuDcC'),
(12, 'Reinaldo', 'Ordoñez', 'el.papayas15@gmail.cl', '$2a$11$06VYZTLp8AE0zNwfro0vruBdyFuGHj1ppkdhvNJFNJ4nxehC6Ta3a'),
(13, 'Mercy', 'Root', 'mercy.Developer@gmail.cl', '$2a$11$r/snF7Tvpa4mikB92qeYTuUeFuGQd4ALB52Vn/V8ssO.6J6rFz.Sm');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`idCliente`);

--
-- Indices de la tabla `datosfichatecnica`
--
ALTER TABLE `datosfichatecnica`
  ADD PRIMARY KEY (`idDatosFichaTecnica`),
  ADD KEY `fk_DatosFichaTecnica_RecepcionEquipo1_idx` (`RecepcionEquipoId`);

--
-- Indices de la tabla `descripcionservicio`
--
ALTER TABLE `descripcionservicio`
  ADD PRIMARY KEY (`idDescServ`),
  ADD KEY `fk_DescripcionServicio_Servicio1_idx` (`Servicio_idServicio`);

--
-- Indices de la tabla `diagnosticosolucion`
--
ALTER TABLE `diagnosticosolucion`
  ADD PRIMARY KEY (`idDiagnosticoSolucion`),
  ADD KEY `fk_DiagnosticoSolucion_DatosFichaTecnica1_idx` (`DatosFichaTecnicaId`);

--
-- Indices de la tabla `recepcionequipo`
--
ALTER TABLE `recepcionequipo`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_RecepcionEquipo_Servicio1_idx` (`IdServicio`),
  ADD KEY `fk_RecepcionEquipo_Cliente1_idx` (`IdCliente`);

--
-- Indices de la tabla `servicio`
--
ALTER TABLE `servicio`
  ADD PRIMARY KEY (`idServicio`),
  ADD KEY `fk_Servicio_Usuario_idx` (`Usuario_idUsuario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `cliente`
--
ALTER TABLE `cliente`
  MODIFY `idCliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `datosfichatecnica`
--
ALTER TABLE `datosfichatecnica`
  MODIFY `idDatosFichaTecnica` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `descripcionservicio`
--
ALTER TABLE `descripcionservicio`
  MODIFY `idDescServ` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `diagnosticosolucion`
--
ALTER TABLE `diagnosticosolucion`
  MODIFY `idDiagnosticoSolucion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `recepcionequipo`
--
ALTER TABLE `recepcionequipo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `servicio`
--
ALTER TABLE `servicio`
  MODIFY `idServicio` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `datosfichatecnica`
--
ALTER TABLE `datosfichatecnica`
  ADD CONSTRAINT `fk_DatosFichaTecnica_RecepcionEquipo1` FOREIGN KEY (`RecepcionEquipoId`) REFERENCES `recepcionequipo` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `descripcionservicio`
--
ALTER TABLE `descripcionservicio`
  ADD CONSTRAINT `fk_DescripcionServicio_Servicio1` FOREIGN KEY (`Servicio_idServicio`) REFERENCES `servicio` (`idServicio`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `diagnosticosolucion`
--
ALTER TABLE `diagnosticosolucion`
  ADD CONSTRAINT `fk_DiagnosticoSolucion_DatosFichaTecnica1` FOREIGN KEY (`DatosFichaTecnicaId`) REFERENCES `datosfichatecnica` (`idDatosFichaTecnica`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `recepcionequipo`
--
ALTER TABLE `recepcionequipo`
  ADD CONSTRAINT `fk_RecepcionEquipo_Cliente1` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`idCliente`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_RecepcionEquipo_Servicio1` FOREIGN KEY (`IdServicio`) REFERENCES `servicio` (`idServicio`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `servicio`
--
ALTER TABLE `servicio`
  ADD CONSTRAINT `fk_Servicio_Usuario` FOREIGN KEY (`Usuario_idUsuario`) REFERENCES `usuario` (`idUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
