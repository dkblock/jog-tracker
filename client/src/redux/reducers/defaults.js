export const onPendingDefault = (state, isFetchingProp = 'isFetching') => {
  state[isFetchingProp] = true;
  state.hasError = false;
};

export const onFulfilledDefault = (state, hasError, isFetchingProp = 'isFetching') => {
  state[isFetchingProp] = false;
  state.hasError = hasError;
};

export const onRejectedDefault = (state, isFetchingProp = 'isFetching') => {
  state[isFetchingProp] = false;
  state.hasError = true;
};
