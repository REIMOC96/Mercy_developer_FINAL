-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-07-2024 a las 00:24:50
-- Versión del servidor: 10.4.25-MariaDB
-- Versión de PHP: 8.1.10

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `cliente`
--

INSERT INTO `cliente` (`idCliente`, `Nombre`, `Apellido`, `Correo`, `Telefono`, `Direccion`, `Estado`) VALUES
(1, 'Juanito', 'Perez', 'juan@gmail.com', '918878789', 'Acacias 999', 1),
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
  `RecepcionEquipoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `datosfichatecnica`
--

INSERT INTO `datosfichatecnica` (`idDatosFichaTecnica`, `FechaInicio`, `FechaFinalizacion`, `PObservacionesRecomendaciones`, `SOInstalado`, `SuiteOfficeInstalada`, `LectorPDFInstalado`, `NavegadorWebInstalado`, `Antivirus Instalado`, `RecepcionEquipoId`) VALUES
(1, '2024-07-18 17:40:00', '2024-07-28 17:40:00', 'SIn registro', 1, 1, 1, 1, '1', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `descripcionservicio`
--

CREATE TABLE `descripcionservicio` (
  `idDescServ` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Servicio_idServicio` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `descripcionservicio`
--

INSERT INTO `descripcionservicio` (`idDescServ`, `Nombre`, `Servicio_idServicio`) VALUES
(1, 'dvgdfgdg', 1),
(2, 'Prueba 22222', 1),
(4, 'ggggggggggggg', 1),
(5, 'uuuuu', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `diagnosticosolucion`
--

CREATE TABLE `diagnosticosolucion` (
  `idDiagnosticoSolucion` int(11) NOT NULL,
  `DescripcionDiagnostico` varchar(1000) NOT NULL,
  `DescripcionSolucion` varchar(1000) DEFAULT NULL,
  `DatosFichaTecnicaId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recepcionequipo`
--

CREATE TABLE `recepcionequipo` (
  `id` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `IdServicio` int(11) NOT NULL,
  `Fecha` datetime DEFAULT NULL,
  `TipoPc` int(11) DEFAULT NULL,
  `Accesorio` varchar(45) DEFAULT NULL,
  `MarcaPc` varchar(45) DEFAULT NULL,
  `ModeloPc` varchar(45) DEFAULT NULL,
  `NSerie` varchar(45) DEFAULT NULL,
  `CapacidadRam` int(11) DEFAULT NULL,
  `TipoAlmacenamiento` int(11) DEFAULT NULL,
  `CapacidadAlmacenamiento` varchar(45) DEFAULT NULL,
  `TipoGpu` int(11) DEFAULT NULL,
  `Grafico` varchar(45) DEFAULT NULL,
  `Estado` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `recepcionequipo`
--

INSERT INTO `recepcionequipo` (`id`, `IdCliente`, `IdServicio`, `Fecha`, `TipoPc`, `Accesorio`, `MarcaPc`, `ModeloPc`, `NSerie`, `CapacidadRam`, `TipoAlmacenamiento`, `CapacidadAlmacenamiento`, `TipoGpu`, `Grafico`, `Estado`) VALUES
(1, 1, 1, '2024-06-16 19:26:00', 1, 'SIn accesorios', 'Asus', 'ROG', '24242424242', 4, 1, '480', 1, 'Intel', '1'),
(2, 2, 1, '2024-06-16 19:28:00', 2, 'Cargador', 'HP', 'G420', '2655557777', 8, 2, '10000', 1, 'Intel', '1'),
(3, 1, 1, '2024-06-16 19:34:00', 1, 'SIn accesorios', 'Generico', 'Generik09', '22211122', 12, 1, '240', 2, 'GTX-200', NULL);

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `servicio`
--

INSERT INTO `servicio` (`idServicio`, `Nombre`, `Precio`, `Sku`, `Usuario_idUsuario`) VALUES
(1, 'Formateo de PC', 30000, 'ASE22', 1),
(2, 'Armado y desarmado de computadora', 50000, 'DES22', 1);

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `Nombre`, `Apellido`, `Correo`, `password`) VALUES
(1, 'Facundo', 'Marin', 'facundo@gmail.com', '123456'),
(2, 'Lucho', 'lucht', 'lucho@gmail.com', '123456'),
(3, 'cayo', 'miranda', 'cayo@gmail.com', '$2a$11$QCoCFVWWEfVd8tl6nmVd8uVz8yRIxezEU1/AsKJYH3ApY4S4ap4te');

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
  MODIFY `idCliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `datosfichatecnica`
--
ALTER TABLE `datosfichatecnica`
  MODIFY `idDatosFichaTecnica` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `descripcionservicio`
--
ALTER TABLE `descripcionservicio`
  MODIFY `idDescServ` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `diagnosticosolucion`
--
ALTER TABLE `diagnosticosolucion`
  MODIFY `idDiagnosticoSolucion` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `recepcionequipo`
--
ALTER TABLE `recepcionequipo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `servicio`
--
ALTER TABLE `servicio`
  MODIFY `idServicio` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

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
