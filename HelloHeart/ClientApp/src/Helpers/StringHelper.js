export function regValidate(val) {
    const myRegExp = /^[ A-Za-z"(),-:/!]*$/;
    if(myRegExp.test(val)) {
      return true;
    }
    return false;
  }
