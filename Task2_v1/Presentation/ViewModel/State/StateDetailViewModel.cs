﻿using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class StateDetailViewModel : IViewModel, IStateDetailViewModel
{
    public ICommand UpdateState { get; set; }

    private readonly IStateModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private int _id;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            RaisePropertyChanged(nameof(Id));
        }
    }

    private int _productId;

    public int ProductId
    {
        get => _productId;
        set
        {
            _productId = value;
            RaisePropertyChanged(nameof(ProductId));
        }
    }

    private int _price;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            RaisePropertyChanged(nameof(Price));
        }
    }

    public StateDetailViewModel(IStateModelOperation? model = null, IErrorInformer? informer = null)
    {
        this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

        this._modelOperation = IStateModelOperation.CreateModelOperation();
        this._informer = informer ?? new PopupErrorInformer();
    }

    public StateDetailViewModel(int id, int productId, int productQuantity, IStateModelOperation? model = null, IErrorInformer? informer = null)
    {
        this.Id = id;
        this.ProductId = productId;
        this.Price = productQuantity;

        this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

        this._modelOperation = IStateModelOperation.CreateModelOperation();
        this._informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            this._modelOperation.UpdateAsync(this.Id, this.ProductId, this.Price);

            this._informer.InformSuccess("State successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(this.Price.ToString()) ||
            this.Price < 0
        );
    }
}