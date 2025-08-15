/* eslint-disable @typescript-eslint/no-explicit-any */
export const getQueryStringParams = (
    paramsObj: Record<string, any>
  ): URLSearchParams => {
    const params = new URLSearchParams();
  
    Object.keys(paramsObj).forEach((key) => {
      const value = paramsObj[key];
  
      if (value) {
        if (Array.isArray(value)) {
          if (value.length > 0) params.append(key, value.join(','));
        } else {
          params.append(key, value.toString());
        }
      }
    });
  
    return params;
  };