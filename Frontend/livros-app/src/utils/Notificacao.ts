import Swal from 'sweetalert2'

function parseTexto(mensagem: string | string[]) {
    let texto = "";
    if (Array.isArray(mensagem)) {
        texto = mensagem.map(msg => `<span>${msg}</span><br />`).join("");
    } else {
        texto = mensagem;
    }
    return texto;
}

class Notificacao {
    sucesso(mensagem: string | string[], callback?: Function) {

        let texto = parseTexto(mensagem);
        Swal.fire({
            title: "Sucesso",
            html: texto,
            icon: 'success',
            confirmButtonText: 'Ok',
            allowOutsideClick: false,
        }).then((result) => {
            if (callback && result.isConfirmed)
                callback();
        })
    }

    confirmacao(mensagem: string, callback?: Function) {
        Swal.fire({
            title: "Atencao",
            html: mensagem,
            icon: "warning",
            allowOutsideClick: false,
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Sim",
            cancelButtonText: "Cancelar"
        }).then((result) => {
            if (result.isConfirmed && callback) {
                callback();
            }
        });
    }

    erro(mensagem: string | string[]) {

        let texto = parseTexto(mensagem);
        Swal.fire({
            title: "Erro",
            html: texto,
            icon: 'error',
            allowOutsideClick: false,
            confirmButtonText: 'Ok'
        })
    }
}

export default new Notificacao();