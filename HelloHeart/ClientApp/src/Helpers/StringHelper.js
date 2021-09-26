export function regValidate(val) {
    const myRegExp = /^[ A-Za-z"(),-:/!]*$/;
    return myRegExp.test(val)
  }

export function enumToText(number) {
    switch (number) {
      case 0: return "Unknown :S"
      case 1: return "Good :)"
      case 2: return "Bad :(";
      default:
        break;
    }
  }
