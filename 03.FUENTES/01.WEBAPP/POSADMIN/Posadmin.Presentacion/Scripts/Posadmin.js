function replace_tildes(str) {
   str = str.replace('£Á', 'Á');
   str = str.replace('Ã‰', 'É');
   str = str.replace('Ã', 'Í');
   str = str.replace('Ã“', 'Ó');
   str = str.replace('Ãš', 'Ú');

   str = str.replace('Ã¡', 'á');
   str = str.replace('Ã©', 'é');
   str = str.replace('Ã­', 'í');
   str = str.replace('Ã³', 'ó');
   str = str.replace('Ãº', 'ú');

   str = str.replace('Ã‘', 'Ñ');
   str = str.replace('Ã±', 'ñ');

   return str;
}
function renderCombo(element, data, label, codigo) {
   var $ele = $(element);
   $ele.empty();
   $ele.append($('<option/>').val('0').text('< Seleccione ' + replace_tildes(label) + ' >'));
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
};
function renderComboSexo(element, label, codigo) {
   var $ele = $(element);
   $ele.empty();
   $ele.append($('<option/>').val('0').text('< Seleccione ' + replace_tildes(label) + ' >'));
   var data = [
    { Value: 'M', Text: 'MASCULINO' },
    { Value: 'F', Text: 'FEMENINO' },
    { Value: 'O', Text: 'OTRO' }
   ];
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
}
function renderComboAnios(element, codigo) {
   var $ele = $(element);
   $ele.empty();
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetAnios',
      success: function (data) {
         if (data != null) {
            $.each(data, function (i, val) {
               if (val.Value == codigo) {
                  $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
               }
               else {
                  $ele.append($('<option/>').val(val.Value).text(val.Text));
               }
            })
         }
      }
   });
};
function renderComboMeses(element, codigo) {
   var $ele = $(element);
   var label = 'Meses';
   $ele.empty();
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetMeses',
      success: function (data) {
         if (data != null) {
            $.each(data, function (i, val) {
               if (val.Value == codigo) {
                  $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
               }
               else {
                  $ele.append($('<option/>').val(val.Value).text(val.Text));
               }
            })
         }
      }
   });
};
function renderComboEstadoHabitaciones(element, label, codigo) {
   var $ele = $(element);
   $ele.empty();
   $ele.append($('<option/>').val('0').text('< Seleccione ' + replace_tildes(label) + ' >'));
   var data = [
    { Value: 'L', Text: 'LIBRE' },
    { Value: 'O', Text: 'OCUPADO' },
    { Value: 'R', Text: 'RESERVADO' }
   ];
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
}
function renderComboTiposPago(element, codigo) {
   var $ele = $(element);
   $ele.empty();
   $ele.append($('<option/>').val('0').text('< Seleccione Tipo >'));
   var data = [
    { Value: 'E', Text: 'ENTRADA' },
    { Value: 'S', Text: 'SALIDA' },
    { Value: 'A', Text: 'ADICIONAL' }
   ];
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
}
function renderComboEstadoReservaciones(element, label, codigo) {
   var $ele = $(element);
   $ele.empty();
   //$ele.append($('<option/>').val('0').text('< Seleccione ' + replace_tildes(label) + ' >'));
   var data = [
    { Value: 'A', Text: 'ACTIVO' },
    { Value: 'X', Text: 'ANULADO' },
    { Value: 'E', Text: 'ENTREGADO' }
   ];
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
}
function renderComboEstadoRegistros(element, label, codigo) {
   var $ele = $(element);
   $ele.empty();
   var data = [
    { Value: 'A', Text: 'ACTIVO' },
    { Value: 'X', Text: 'ANULADO' },
    { Value: 'C', Text: 'CANCELADO' }
   ];
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
}
function renderComboTramos(element, label, codigo) {
   var $ele = $(element);
   $ele.empty();
   var data = [
    { Value: 'D', Text: 'DIAS' },
    { Value: 'H', Text: 'HORAS' }
   ];
   if (data != null) {
      $.each(data, function (i, val) {
         if (val.Value == codigo) {
            $ele.append($('<option/>').val(val.Value).text(val.Text).selected(true));
         }
         else {
            $ele.append($('<option/>').val(val.Value).text(val.Text));
         }
      })
   }
}
function renderComboPaises(element, label, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetPaises',
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboUbigeosPadres(element, label, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetUbigeosPadres',
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboUbigeo(elementDepartamento, labelDepartamento, CodigoDepartamento, elementProvincia, labelProvincia, CodigoProvincia, elementDistrito, labelDistrito, CodigoDistrito, CodigoPais) {
   renderComboUbigeoDepartamento(elementDepartamento, labelDepartamento, CodigoDepartamento, CodigoPais);
   renderComboUbigeoProvincia(elementProvincia, labelProvincia, CodigoDepartamento, CodigoProvincia, CodigoPais);
   renderComboUbigeoDistrito(elementDistrito, labelDistrito, CodigoProvincia, CodigoDistrito, CodigoPais);
};
function renderComboUbigeoDepartamento(element, label, codigo, pais) {
   if (pais != '0') {
      $.ajax({
         type: "GET",
         url: '/PosadminWebApp/api/Consultas/GetUbigeosPorNivel',
         data: { 'InternoPadre': 0, 'InternoPais': pais },
         success: function (data) {
            renderCombo(element, data, label, codigo)
         }
      });
   }
   else {
      renderCombo(element, null, label, codigo);
   }
};
function renderComboUbigeoProvincia(element, label, ubigeo, codigo, pais) {
   if (pais != '0' && ubigeo != '0') {
      $.ajax({
         type: "GET",
         url: '/PosadminWebApp/api/Consultas/GetUbigeosPorNivel',
         data: { 'InternoPadre': ubigeo, 'InternoPais': pais },
         success: function (data) {
            renderCombo(element, data, label, codigo)
         }
      });
   }
   else {
      renderCombo(element, null, label, codigo);
   }
};
function renderComboUbigeoDistrito(element, label, ubigeo, codigo, pais) {
   if (pais != '0' && ubigeo != '0') {
      $.ajax({
         type: "GET",
         url: '/PosadminWebApp/api/Consultas/GetUbigeosPorNivel',
         data: { 'InternoPadre': ubigeo, 'InternoPais': pais },
         success: function (data) {
            renderCombo(element, data, label, codigo)
         }
      });
   }
   else {
      renderCombo(element, null, label, codigo);
   }
};
function renderComboRoles(element, label, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetRoles',
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboTablas(element, label, empresa, tabla, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetTablas',
      data: { 'InternoEmpresa': empresa, 'Tabla': tabla },
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboTablasActivas(element, label, empresa, tabla, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetTablasActivas',
      data: { 'InternoEmpresa': empresa, 'Tabla': tabla },
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboUsuarios(element, label, empresa, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetUsuariosPorEmpresa',
      data: { 'InternoEmpresa': empresa },
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboSucursales(element, label, empresa, codigo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetSucursalesPorEmpresa',
      data: { 'InternoEmpresa': empresa },
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboHabitaciones(element, label, empresa, sucursal, codigo, piso, tipo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetHabitacionesLibres',
      data: { 'InternoEmpresa': empresa, 'InternoSucursal': sucursal, 'InternoPIS': piso, 'InternoTHA': tipo },
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function renderComboHabitacionesTodos(element, label, empresa, sucursal, codigo, piso, tipo) {
   $.ajax({
      type: "GET",
      url: '/PosadminWebApp/api/Consultas/GetHabitaciones',
      data: { 'InternoEmpresa': empresa, 'InternoSucursal': sucursal, 'InternoPIS': piso, 'InternoTHA': tipo },
      success: function (data) {
         renderCombo(element, data, label, codigo)
      }
   });
};
function getRandomColor() {
   var letters = '0123456789ABCDEF';
   var color = '#';
   for (var i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
   }
   return color;
}
