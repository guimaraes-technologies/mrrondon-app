﻿using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Behaviors
{
    //public class InfiniteScrollBehavior : Behavior<ListView>
    //{
    //    public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create("LoadMoreCommand", typeof(ICommand), typeof(InfiniteScrollBehavior), null);

    //    public ICommand LoadMoreCommand
    //    {
    //        get => (ICommand)GetValue(LoadMoreCommandProperty);
    //        set => SetValue(LoadMoreCommandProperty, value);
    //    }

    //    public ListView AssociatedObject { get; private set; }

    //    protected override void OnAttachedTo(ListView bindable)
    //    {
    //        base.OnAttachedTo(bindable);
    //        AssociatedObject = bindable;
    //        bindable.BindingContextChanged += Bindable_BindingContextChanged;
    //        bindable.ItemAppearing += InfiniteListView_ItemAppearing;
    //    }

    //    private void Bindable_BindingContextChanged(object sender, EventArgs e)
    //    {
    //        OnBindingContextChanged();
    //    }

    //    protected override void OnBindingContextChanged()
    //    {
    //        base.OnBindingContextChanged();
    //        BindingContext = AssociatedObject.BindingContext;
    //    }

    //    protected override void OnDetachingFrom(ListView bindable)
    //    {
    //        base.OnDetachingFrom(bindable);
    //        bindable.BindingContextChanged -= Bindable_BindingContextChanged;
    //        bindable.ItemAppearing -= InfiniteListView_ItemAppearing;
    //    }

    //    private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
    //    {
    //        if (!(AssociatedObject.ItemsSource is IList items) || e.Item != items[items.Count - 1]) return;

    //        if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null)) LoadMoreCommand.Execute(null);
    //    }
    //}
}